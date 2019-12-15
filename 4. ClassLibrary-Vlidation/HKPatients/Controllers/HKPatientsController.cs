using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HKPatients.Models;

namespace HKPatients.Controllers
{
    public class HKPatientsController : Controller
    {
        private readonly HKPatientsContext _context;

        public HKPatientsController(HKPatientsContext context)
        {
            _context = context;
        }

        // GET: HKPatients
        public async Task<IActionResult> Index()
        {            
            var hKPatientsContext = _context
                            .Patient
                            .Include(p => p.PatientDiagnosis)
                            .Include(p => p.ProvinceCodeNavigation)
                            .OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
            return View(await hKPatientsContext.ToListAsync());
        }

        // GET: HKPatients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.ProvinceCodeNavigation)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: HKPatients/Create
        public IActionResult Create()
        {
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode");
            return View();
        }

        // POST: HKPatients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fullname,PatientId,FirstName,LastName,Address,City,ProvinceCode,PostalCode,Ohip,DateOfBirth,Deceased,DateOfDeath,HomePhone,Gender")] Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(patient);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Successfully created!!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                //   throw;
                ModelState.AddModelError("", ex.GetBaseException().Message);
                return View(patient);
            }
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", patient.ProvinceCode);
            return View(patient);
        }

        // GET: HKPatients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["ProvinceCode"] = new SelectList(_context.Province.OrderBy(o => o.Name), 
                "ProvinceCode", "ProvinceCode", patient.ProvinceCode);
            return View(patient);
        }

        // POST: HKPatients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Fullname,PatientId,FirstName,LastName,Address,City,ProvinceCode,PostalCode,Ohip,DateOfBirth,Deceased,DateOfDeath,HomePhone,Gender")] Patient patient)
        {
            if (id != patient.PatientId)
            {
                ModelState.AddModelError("PatientID", "No Patient to edit.");
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Successfully updated!!";
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!PatientExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        //   throw;
                        ModelState.AddModelError("", ex.GetBaseException().Message);
                        return View(patient);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", patient.ProvinceCode);
            return View(patient);
        }

        // GET: HKPatients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.ProvinceCodeNavigation)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: HKPatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.FindAsync(id);
            try
            {
                _context.Patient.Remove(patient);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Successfully deleted!!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //   throw;
                TempData["Message"] = ex.GetBaseException().Message;
                return View(patient);
            }
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.PatientId == id);
        }
    }
}
