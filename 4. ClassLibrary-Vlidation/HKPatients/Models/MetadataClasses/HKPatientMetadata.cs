/*
 * PROG2230 Assignment4
 * 
 * Purpose: Self-Validation
 * 
 * Revision History
 *    Nov 17, 2019 - Heuijin Ko Created
 * 
 */
using HKClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HKPatients.Models
{
    [ModelMetadataType(typeof(HKPatientMetadata))]

    public partial class Patient : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // variables
            HKPatientsContext _context = (HKPatientsContext)validationContext.GetService(typeof(HKPatientsContext));
            string countryCode = "";

            // Capitalize 
            FirstName = HKValidations.HKCapitalize((FirstName + "").Trim());
            LastName = HKValidations.HKCapitalize((LastName + "").Trim());
            Gender = HKValidations.HKCapitalize((Gender + "").Trim());
            Address = HKValidations.HKCapitalize((Address + "").Trim());
            City = HKValidations.HKCapitalize((City + "").Trim());
            Ohip = (Ohip + "").Trim().ToUpper();
            PostalCode = (PostalCode + "").Trim();
            ProvinceCode = (ProvinceCode + "").Trim().ToUpper();
            HomePhone = (HomePhone + "").Trim();


            if (FirstName == "")
                yield return new ValidationResult("First name cannot be empty or just blanks",
                                                    new[] { nameof(FirstName) });

            if (LastName == "")
                yield return new ValidationResult("Last Name cannot be empty or just blanks",
                                                    new[] { nameof(LastName) });

            if (Gender == "")
                yield return new ValidationResult("Gender cannot be empty or just blanks",
                                                    new[] { nameof(Gender) });
            else if (Gender.Trim() != "M" && Gender.Trim() != "F" && Gender.Trim() != "X")
                yield return new ValidationResult("Gender must be either 'M', 'F', or 'X'",
                                                    new[] { nameof(Gender) });


            // 2.e validate ProvinceCode
            if (ProvinceCode == "")
                yield return new ValidationResult("Province Code cannot be empty or just blanks",
                                                    new[] { nameof(ProvinceCode) });
            else
            {
                // 2.e.i, Validate ProvinceCode from the database
                var _provinceContext = _context.Province.Where(e => e.ProvinceCode == ProvinceCode).SingleOrDefault();

                // e.ii if feetching province code throws an exception, show message
                if (_provinceContext != null)
                    countryCode = _provinceContext.CountryCode;
                else
                    yield return new ValidationResult("Province code is not on file", new[] { nameof(ProvinceCode) });
            }

            // 2.f validate PostalCode
            if (PostalCode != "")
            {
                // 2.f.i
                if (ProvinceCode == "" || countryCode == "")
                    yield return new ValidationResult("Province code is required to validate Postal Code", new[] { nameof(PostalCode) });
                else
                {                    
                    // 2.f.ii
                    if (countryCode == "CA")
                    {
                        string theFirstLetter = _context.Province.FirstOrDefault(p => p.ProvinceCode == ProvinceCode).FirstPostalLetter;
                        if (!theFirstLetter.Contains(PostalCode.Substring(0, 1).ToUpper()))
                        {
                            yield return new ValidationResult("First letter of Postal Code not valid for given province",
                                                      new[] { nameof(PostalCode), nameof(ProvinceCode) });
                        }
                        // 2.f.iii
                        if (!HKValidations.HKPostalCodeValidation(PostalCode))
                            yield return new ValidationResult("Postal code is not CDN pattern: A3A 3A3",
                                                         new[] { nameof(PostalCode) });

                        PostalCode = HKValidations.HKPostalCodeFormat(PostalCode);
                    }
                    else
                    {
                        // 2.f.iii
                        string zipCode = PostalCode;
                        if (!HKValidations.HKZipCodeValidation(ref zipCode))
                            yield return new ValidationResult("Postal code is not valid(3)",
                                                        new[] { nameof(PostalCode) });
                        PostalCode = zipCode;
                    }
                }
            }

            // 2.g validate Ohip
            if (Ohip != "")
            {
                Regex pattern = new Regex(@"^[0-9]{4}[-][0-9]{3}[-][0-9]{3}[-][A-Za-z]{2}$", RegexOptions.IgnoreCase);
                if (!pattern.IsMatch(Ohip))
                    yield return new ValidationResult("OHIP, if provided, must match patter: 1234-123-123-XX",
                                                        new[] { nameof(Ohip) });
            }

            // 2.h validate HomePhone
            if (HomePhone != "")
            {
                string extractedHomePhone = HKValidations.HKExtractDigits(HomePhone);
                if (extractedHomePhone.Length != 10)
                    yield return new ValidationResult("Home phone, if provided, must be 10 digits: 123-123-1234",
                                                        new[] { nameof(HomePhone) });
                else
                {
                    Regex pattern = new Regex(@"^[0-9]{3}[-]?[0-9]{3}[-]?[0-9]{4}$", RegexOptions.IgnoreCase);
                    if (!pattern.IsMatch(HomePhone))
                        yield return new ValidationResult("Home phone, if provided, must be 10 digits: 123-123-1234",
                                                                        new[] { nameof(HomePhone) });
                }
                HomePhone = (extractedHomePhone.Insert(3, "-")).Insert(7, "-");
            }

            // 2.i validate DateOfBirth
            if (DateOfBirth != null)
            {
                if (DateOfBirth > DateTime.Now)
                    yield return new ValidationResult("Date of Birth can not be in the future",
                                                            new[] { nameof(DateOfBirth) });
            }

            if (DateOfDeath > DateTime.Now)
                yield return new ValidationResult("Date of Death can not be in the future",
                                                        new[] { nameof(DateOfDeath) });

            
            if (Deceased)
            {
                // 2.i.i If deceased is true, dateOfDeath is required
                if (DateOfDeath == null)
                    yield return new ValidationResult("if Deceased is true, a Date of Death is required",
                                                            new[] { nameof(DateOfDeath) });
                // 2.i.iii before dateOfBirth (if it is provided)
                else // DateOfDeath != null
                {
                    if (DateOfDeath < DateOfBirth)
                        yield return new ValidationResult("The Date of Death cannot be earlier than the Date of Birth",
                                                                new[] { nameof(DateOfDeath) });
                }
            }
            // 2.i.ii.	If deceased is false, dateOfDeath must be null.
            else
            {
                if (DateOfDeath != null)
                    yield return new ValidationResult("Deceased must be true if Date if Death is provided",
                                                                new[] { nameof(Deceased) });
            }
        }
    }
    public class HKPatientMetadata
    {
        public int PatientId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Street Address")]
        public string Address { get; set; }
        public string City { get; set; }

        [Display(Name = "Province Code")]
        public string ProvinceCode { get; set; }

        [StringLength(7)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "OHIP")]
        public string Ohip { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public bool Deceased { get; set; }
        [Display(Name = "Date of Death")]
        public DateTime? DateOfDeath { get; set; }
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }
        [Required]
        public string Gender { get; set; }

        public virtual Province ProvinceCodeNavigation { get; set; }
        public virtual ICollection<PatientDiagnosis> PatientDiagnosis { get; set; }
    }
}
