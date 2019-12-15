using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consulting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Consulting.Controllers
{
    public class RemoteController : Controller
    {
        private readonly ConsultingContext _context;

        public RemoteController(ConsultingContext context)
        {
            _context = context;
        }

        public JsonResult OrderDateNoFutureOrBeforeContracDate(DateTime DateWorked)
        {
            int id = (int)HttpContext.Session.GetInt32("ContractID");
            DateTime startDate = _context.Contract.Where(w => w.ContractId == id).FirstOrDefault().StartDate;
            Boolean isFutureOrBeforeContractDate = DateWorked >= DateTime.Now || DateWorked <= startDate;
            if (isFutureOrBeforeContractDate)
            {
                return Json($"Date worked is either in the future or before contract started!!");
            }
            else
            {
                return Json(true);
            }
        }

        public JsonResult ChcekConsultantID(int ConsultantId)
        {
            if(_context.WorkSession.Where(a => a.ConsultantId == ConsultantId).Any())
            {
                return Json(true);
            }
            else
            {
                return Json($"Consultant id {ConsultantId} not found!");
            }
        }
    }
}