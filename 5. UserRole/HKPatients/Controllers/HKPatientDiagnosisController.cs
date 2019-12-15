/*
 * PROG2230 Assignment3
 * 
 * Purpose: Practice Filtering & Dropdowns of MVC model
 * 
 * Revision History
 *    Oct 21, 2019 - Heuijin Ko Created
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HKPatients.Models;
using Microsoft.AspNetCore.Http;

namespace HKPatients.Controllers
{
    public class HKPatientDiagnosisController : Controller
    {
        private readonly HKPatientsContext _context;

        public HKPatientDiagnosisController(HKPatientsContext context)
        {
            _context = context;
        }

        // GET: PatientDiagnosis
        public async Task<IActionResult> Index(int? id)
        {
            await HttpContext.Session.LoadAsync();
            int patientID; 
            string fullName;
            if (id != null)
            {
                patientID = (int)id;
                HttpContext.Session.SetInt32("PID", (int)id);
            }
            else if (HttpContext.Session.GetInt32("PID") == null)
            {
                TempData["Message"] = "Select a patient to see.";
                return RedirectToAction("Index", "HKPatients");
            }            
            else
            {
                id = HttpContext.Session.GetInt32("PID");
            }

            var User = await _context.Patient
                                .SingleOrDefaultAsync(s => s.PatientId == id);

            if(User == null)
            {
                TempData["Message"] = "No patient to see.";
                return RedirectToAction("Index", "HKPatients");
            }

            fullName = User.LastName + ", " + User.FirstName;
            HttpContext.Session.SetString("FullName", fullName);

            var PDRecord = await _context
                                        .PatientDiagnosis
                                        .Include(p => p.Diagnosis)
                                        .Include(p => p.Patient)
                                        .Where(p => p.PatientId == id)
                                        .OrderBy(p => p.Patient.LastName).ThenBy(p => p.Patient.FirstName)
                                        .OrderByDescending(p => p.PatientDiagnosisId)
                                        .ToListAsync();

            if(PDRecord.Any())
            {
                HttpContext.Session.SetInt32("DID", PDRecord.FirstOrDefault().DiagnosisId);
            }
            return View(PDRecord);
        }

        // GET: PatientDiagnosis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientDiagnosis = await _context.PatientDiagnosis
                .Include(p => p.Diagnosis)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PatientDiagnosisId == id);
            if (patientDiagnosis == null)
            {
                return NotFound();
            }

            return View(patientDiagnosis);
        }

        // GET: PatientDiagnosis/Create
        public IActionResult Create()
        {
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnosis, "DiagnosisId", "Name");
            return View();
        }

        // POST: PatientDiagnosis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientDiagnosisId,PatientId,DiagnosisId,Comments")] PatientDiagnosis patientDiagnosis)
        {
            string PID = HttpContext.Session.GetInt32("PID").ToString();
            if(string.IsNullOrEmpty(PID))
            {
                ModelState.AddModelError("PatientId", "No patient to create.");
                return RedirectToAction("Index", "HKPatients");
            }
            patientDiagnosis.PatientId = Convert.ToInt32(PID);
            if (ModelState.IsValid)
            {
                _context.Add(patientDiagnosis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnosis, "DiagnosisId", "Name", patientDiagnosis.DiagnosisId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "FirstName", patientDiagnosis.PatientId);
            return View(patientDiagnosis);
        }

        // GET: PatientDiagnosis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientDiagnosis = await _context.PatientDiagnosis.FindAsync(id);
            if (patientDiagnosis == null)
            {
                return NotFound();
            }
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnosis, "DiagnosisId", "Name", patientDiagnosis.DiagnosisId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "FirstName", patientDiagnosis.PatientId);
            return View(patientDiagnosis);
        }

        // POST: PatientDiagnosis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientDiagnosisId,PatientId,DiagnosisId,Comments")] PatientDiagnosis patientDiagnosis)
        {
            if (id != patientDiagnosis.PatientDiagnosisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientDiagnosis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientDiagnosisExists(patientDiagnosis.PatientDiagnosisId))
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
            ViewData["DiagnosisId"] = new SelectList(_context.Diagnosis, "DiagnosisId", "Name", patientDiagnosis.DiagnosisId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "FirstName", patientDiagnosis.PatientId);
            return View(patientDiagnosis);
        }

        // GET: PatientDiagnosis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientDiagnosis = await _context.PatientDiagnosis
                .Include(p => p.Diagnosis)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PatientDiagnosisId == id);
            if (patientDiagnosis == null)
            {
                return NotFound();
            }

            return View(patientDiagnosis);
        }

        // POST: PatientDiagnosis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientDiagnosis = await _context.PatientDiagnosis.FindAsync(id);
            _context.PatientDiagnosis.Remove(patientDiagnosis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientDiagnosisExists(int id)
        {
            return _context.PatientDiagnosis.Any(e => e.PatientDiagnosisId == id);
        }
    }
}
