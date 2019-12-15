using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consulting.Models
{
    [ModelMetadataType(typeof(WorkSessionMetadata))]
    public partial class WorkSession : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //ConsultingContext _context = new ConsultingContext();
            ConsultingContext _context = (ConsultingContext)validationContext.GetService(typeof(ConsultingContext));

            var results = new List<ValidationResult>();

            double dbHourlyRate = _context.Consultant
                                            .Where(w => w.ConsultantId == ConsultantId)
                                            .FirstOrDefault().HourlyRate;

            if (_context.Consultant.Where(w=>w.ConsultantId == ConsultantId).Any())
            {
                if (0 > HoursWorked || HoursWorked > 24)
                    yield return new ValidationResult("must be greater than zero and total no more than 24", new[] { nameof(HoursWorked) });

                if (HourlyRate == 0.0)
                {
                    HourlyRate = dbHourlyRate;
                }
                else if (HourlyRate < 0.0 || HourlyRate > 1.5*dbHourlyRate)
                {
                    results.Add(new ValidationResult("Hourly rate should be greater than 0 and less than 1.5 Consultant hourly rate!"));
                    yield return new ValidationResult("Hourly rate should be greater than 0 and less than 1.5 Consultant hourly rate", new[] { nameof(HourlyRate) });
                }

                TotalChargeBeforeTax = HourlyRate * HoursWorked;

                var customerId = _context.Contract.Where(w => w.ContractId == ContractId).FirstOrDefault().CustomerId;
                var provinceCode = _context.Customer.Where(w => w.CustomerId == customerId).FirstOrDefault().ProvinceCode;
                var provincdTax = _context.Province.Where(w => w.ProvinceCode == provinceCode).FirstOrDefault().ProvincialTax;

                ProvincialTax = TotalChargeBeforeTax * provincdTax;
            }
            
            yield return ValidationResult.Success;

        }

        
    }

      public class WorkSessionMetadata
    {
        [Display(Name = "Work Session")]
        public int WorkSessionId { get; set; }
        [Display(Name = "Contract ID")]
        public int ContractId { get; set; }

        [Required]
        [Remote("OrderDateNoFutureOrBeforeContracDate", "Remote")]
        [Display(Name = "Date Worked")]
        public DateTime DateWorked { get; set; }
        [Display(Name = "Consultant Id")]
        [Remote("ChcekConsultantID", "Remote")]
        public int ConsultantId { get; set; }
        [Display(Name = "Hours Worked")]
        public double HoursWorked { get; set; }
        [Display(Name = "Description")]
        public string WorkDescription { get; set; }
        [Display(Name = "Hourly Rate")]
        [DisplayFormat(DataFormatString="{0:c2}")]
        public double HourlyRate { get; set; }
        [Display(Name = "Provincial Tax")]
        [DisplayFormat(DataFormatString="{0:c2}")]
        public double ProvincialTax { get; set; }
        [Display(Name = "Total Cost")]
        [DisplayFormat(DataFormatString="{0:c2}")]
        public double TotalChargeBeforeTax { get; set; }

    }
}
