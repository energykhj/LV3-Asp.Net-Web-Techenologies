using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HKPatients.Models;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace HKPatients.Controllers
{
    public class HKRemoteController : Controller
    {
        private readonly HKPatientsContext _context;

        public HKRemoteController(HKPatientsContext context)
        {
            _context = context;
        }

        public JsonResult ProvinceCode(string ProvinceCode)
        {
            var province = _context.Province
                                .Where(a => a.ProvinceCode == ProvinceCode)
                                .FirstOrDefault();

            try
            {
                if (province == null)
                    return Json("Province code is not on file2");
                else
                {
                    var countryCode = province.CountryCode;
                    HttpContext.Session.SetString("countryCode", countryCode);
                    return Json(true);
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public JsonResult PostalCode(string postalCode, string provinceCode)
        {
            postalCode = (postalCode + "").Trim();

            if (postalCode == "")
                return Json(true);
            
            string _countryCode = _context.Province.FirstOrDefault(x => x.ProvinceCode == provinceCode).CountryCode;
            
            try
            {
                var pattern = _context.Country.FirstOrDefault(x => x.CountryCode == _countryCode).PostalPattern;
                Regex regexPattern = new Regex(pattern, RegexOptions.IgnoreCase);

                if (regexPattern.IsMatch(postalCode))
                    return Json(true);

                else
                    return Json("Postal Code is not on file2");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public JsonResult PostalCode1(string postalCode)
        {
             postalCode = (postalCode + "").Trim();

           if (postalCode == "")
                return Json(true);

            if (HttpContext.Session.GetString("countryCode") == null)
                return Json("Needs CountryCode");

            string countryCode = HttpContext.Session.GetString("countryCode");

            try
            {
                var pattern = _context.Country.FirstOrDefault(x => x.CountryCode == countryCode).PostalPattern;
                Regex regexPattern = new Regex(pattern, RegexOptions.IgnoreCase);

                if (regexPattern.IsMatch(postalCode))
                    return Json(true);

                else
                    return Json("Postal Code is not on file2");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}