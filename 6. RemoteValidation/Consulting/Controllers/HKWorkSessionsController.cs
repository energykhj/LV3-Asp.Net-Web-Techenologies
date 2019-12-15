using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Consulting.Models;

namespace Consulting.Controllers
{
    public class HKWorkSessionsController : Controller
    {
        private readonly ConsultingContext _context;

        public HKWorkSessionsController(ConsultingContext context)
        {
            _context = context;
        }

        // GET: HKWorkSessions
        public async Task<IActionResult> Index(int? id)
        {
            if(id != null)
            {
                HttpContext.Session.SetInt32("ContractID", (int)id);
            }
            else
            {
                if(HttpContext.Session.GetInt32("ContractID") == null)
                {
                    TempData["Message"] = "No record to show";
                    return RedirectToAction("Index", "HKContract");
                }
                else
                {
                    id = HttpContext.Session.GetInt32("ContractID");
                }
            }

            if (!_context.WorkSession.Where(a => a.ContractId == id).Any())
            {
                //var contracName = _context.Contract.Where(a => a.ContractId == id).Select(a => a.Name).FirstOrDefaultAsync();
                var contractName = _context.Contract.Where(a => a.ContractId == id).FirstOrDefault().Name;
                TempData["Message"] = $"no work session exist for contract {contractName}";

                return View();
            }

            //var consultingContext = _context.WorkSession
            //                            .Include(w => w.Consultant)
            //                            .Include(w => w.Contract)
            //                            .Where(w => w.ContractId == id);
            var consultingContext =  _context.WorkSession
                                        .Include(w => w.Consultant)
                                        .Include(w => w.Contract)
                                        .Where(w => w.ContractId == id);


            double totalHour = 0.0, totalCost = 0.0, provincialTax = 0.0;

            var customerID = _context.Contract.Where(a => a.ContractId == id).FirstOrDefault().CustomerId;
            var customerProvince = _context.Customer.Where(a => a.CustomerId == customerID).FirstOrDefault().ProvinceCode;
            provincialTax = Convert.ToDouble(_context.Province.Where(a => a.ProvinceCode ==  customerProvince).FirstOrDefault().ProvincialTax);

            foreach (var item in consultingContext)
            {
                totalHour += (double)item.HoursWorked;
                totalCost += (double)item.HourlyRate;
            }

            totalCost = totalHour * totalCost * (1.0+provincialTax);
            TempData["Message"] = $"Total hours {totalHour} and total cost {string.Format("{0:c2}",totalCost)}";
            ViewBag.provincialTax = provincialTax;
            return View(await consultingContext.ToListAsync());
        }

        // GET: HKWorkSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSession = await _context.WorkSession
                .Include(w => w.Consultant)
                .Include(w => w.Contract)
                .FirstOrDefaultAsync(m => m.WorkSessionId == id);
            if (workSession == null)
            {
                return NotFound();
            }

            return View(workSession);
        }

        // GET: HKWorkSessions/Create
        public IActionResult Create()
        {
            int contractID = (int)HttpContext.Session.GetInt32("ContractID");

            if(_context.Contract.Where(w => w.ContractId == contractID && w.Closed).Any())
            {
                TempData["Message"] = "Work session cannot be created because contract was closed";
                //return RedirectToAction("Index", "HKWorkSesstion");
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ConsultantId"] = new SelectList(_context.Consultant, "ConsultantId", "FirstName");
            //ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "Name");

            WorkSession ws = new WorkSession();
            ws.ContractId = contractID;
            ws.HourlyRate = 0;
            ws.ProvincialTax = 0;
            ws.TotalChargeBeforeTax = 0;

            return View(ws);
        }

        // POST: HKWorkSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkSessionId,ContractId,DateWorked,ConsultantId,HoursWorked,WorkDescription,HourlyRate,ProvincialTax,TotalChargeBeforeTax")] WorkSession workSession)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(workSession);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "added";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Create exception: " + ex.GetBaseException().Message;
            }
            //ViewData["ConsultantId"] = new SelectList(_context.Consultant, "ConsultantId", "FirstName", workSession.ConsultantId);
            //ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "Name", workSession.ContractId);
            return View(workSession);
        }

        // GET: HKWorkSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int contractID = (int)HttpContext.Session.GetInt32("ContractID");

            if (_context.Contract.Where(a=>a.ContractId == contractID && a.Closed).Any())
            {
                TempData["Message"] = "No work session exist because contract is closed";
                return RedirectToAction(nameof(Index));
            }

            var workSession = await _context.WorkSession.FindAsync(id);
            if (workSession == null)
            {
                return NotFound();
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "ConsultantId", "FirstName", workSession.ConsultantId);
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "Name", workSession.ContractId);
            return View(workSession);
        }

        // POST: HKWorkSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkSessionId,ContractId,DateWorked,ConsultantId,HoursWorked,WorkDescription,HourlyRate,ProvincialTax,TotalChargeBeforeTax")] WorkSession workSession)
        {
            if (id != workSession.WorkSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    try
                    {
                        _context.Update(workSession);
                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Updated";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!WorkSessionExists(workSession.WorkSessionId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.GetBaseException().Message;
                }
            }
            //ViewData["ConsultantId"] = new SelectList(_context.Consultant, "ConsultantId", "FirstName", workSession.ConsultantId);
            //ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "Name", workSession.ContractId);
            return View(workSession);
        }

        // GET: HKWorkSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSession = await _context.WorkSession
                .Include(w => w.Consultant)
                .Include(w => w.Contract)
                .FirstOrDefaultAsync(m => m.WorkSessionId == id);
            if (workSession == null)
            {
                return NotFound();
            }

            return View(workSession);
        }

        // POST: HKWorkSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workSession = await _context.WorkSession.FindAsync(id);
            _context.WorkSession.Remove(workSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkSessionExists(int id)
        {
            return _context.WorkSession.Any(e => e.WorkSessionId == id);
        }
    }
}
