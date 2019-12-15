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
    // HKPatientTreatmentController accesses and maintains the PatientTreatment table
    // Heuijin Ko(8187452) Sept 13, 2019
    public class HKPatientTreatmentController : Controller
    {
        private readonly HKPatientsContext _context;

        public HKPatientTreatmentController(HKPatientsContext context)
        {
            _context = context;
        }

        // Display all records of dispensingUnit table
        // GET: HKPatientTreatment
        public async Task<IActionResult> Index(int? pdid, string dname, string fname)
        {
            if (pdid != null)
            {
                HttpContext.Session.SetInt32("PDID", (int)pdid);
                HttpContext.Session.SetString("FullName", fname);
                HttpContext.Session.SetString("DName", dname);
            }
            else if (HttpContext.Session.GetInt32("PDID") == null)
            {
                TempData["Message"] = "select a patient's diagnosis to see.";
                return RedirectToAction("Index", "HKPatientDiagnosis");
            }

            pdid = HttpContext.Session.GetInt32("PDID");

            var hKPatientsContext = _context.PatientTreatment
                                .Include(p => p.PatientDiagnosis)
                                .ThenInclude(d => d.Diagnosis)
                                .Include(t => t.Treatment)
                                .Where(p => p.PatientDiagnosisId == pdid)
                                .OrderByDescending(p => p.TreatmentId);

            return View(await hKPatientsContext.ToListAsync());
        }

        // Display details of selected id
        // id : selected Patient Treatment Id
        // GET: HKPatientTreatment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientTreatment = await _context.PatientTreatment
                .Include(p => p.PatientDiagnosis)
                .Include(p => p.Treatment)
                .FirstOrDefaultAsync(m => m.PatientTreatmentId == id);
            if (patientTreatment == null)
            {
                return NotFound();
            }

            return View(patientTreatment);
        }
        // Setup Create
        // Provide an empty page for user to enter a new treatment
        // GET: HKPatientTreatment/Create
        public IActionResult Create()
        {            
            int DID = getDID();
            if (getDID() < 0)    return RedirectToAction("Index", "HKPatients");

            ViewData["PatientDiagnosisId"] = new SelectList(_context.PatientDiagnosis, "PatientDiagnosisId", "PatientDiagnosisId");
            ViewData["TreatmentId"] = new SelectList(_context.Treatment.Where(b => b.DiagnosisId == DID).OrderBy(a => a.Name), "TreatmentId", "Name");

            PatientTreatment pt = new PatientTreatment();
            pt.DatePrescribed = DateTime.Now;

            return View(pt);
        }

        // POST: HKPatientTreatment/Create
        // Insert the new treatment record if it passes the edits
        // If errors occur, return it to the page with the error messages
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientTreatmentId,TreatmentId,DatePrescribed,Comments,PatientDiagnosisId")] PatientTreatment patientTreatment)
        {
            int DID = getDID();
            if (getDID() < 0)
            {
                ModelState.AddModelError("DiagnosId", "No Diagnos to create.");
                return RedirectToAction("Index", "HKPatientDiagnosis");
            }

            patientTreatment.PatientDiagnosisId = (int)HttpContext.Session.GetInt32("PDID");

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(patientTreatment);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"New record is created.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.GetBaseException().Message);
            }

            ViewData["PatientDiagnosisId"] = new SelectList(_context.PatientDiagnosis, "PatientDiagnosisId", "PatientDiagnosisId", patientTreatment.PatientDiagnosisId);
            ViewData["TreatmentId"] = new SelectList(_context.Treatment.Where(b => b.DiagnosisId == DID).OrderBy(a => a.Name), "TreatmentId", "Name");

            return View(patientTreatment);
        }

        // Retrieve the selected treatment record and display it for update
        // GET: HKPatientTreatment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientTreatment = await _context.PatientTreatment.FindAsync(id);
            if (patientTreatment == null)
            {
                return NotFound();
            }
            
            int DID = (int)HttpContext.Session.GetInt32("DID");
            ViewData["PatientDiagnosisId"] = new SelectList(_context.PatientDiagnosis, "PatientDiagnosisId", "PatientDiagnosisId", patientTreatment.PatientDiagnosisId);
            ViewData["TreatmentId"] = new SelectList(_context.Treatment.Where(b => b.DiagnosisId == DID).OrderBy(a => a.Name), "TreatmentId", "Name", patientTreatment.TreatmentId);

            return View(patientTreatment);
        }

        // POST: HKPatientTreatment/Edit/5
        // Write the updated treatment record to file if it passes the edits
        // If errors occur, load the record back on the view and display error messages
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientTreatmentId,TreatmentId,DatePrescribed,Comments,PatientDiagnosisId")] PatientTreatment patientTreatment)
        {
            if (id != patientTreatment.PatientTreatmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientTreatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientTreatmentExists(patientTreatment.PatientTreatmentId))
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

            int DID = (int)HttpContext.Session.GetInt32("DID");

            ViewData["PatientDiagnosisId"] = new SelectList(_context.PatientDiagnosis, "PatientDiagnosisId", "PatientDiagnosisId", patientTreatment.PatientDiagnosisId);
            ViewData["TreatmentId"] = new SelectList(_context.Treatment.Where(b => b.DiagnosisId == DID).OrderBy(a => a.Name), "TreatmentId", "Name", patientTreatment.TreatmentId);

            return View(patientTreatment);
        }
        // Display the selected treatment record for delete confirmation
        // GET: HKPatientTreatment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientTreatment = await _context.PatientTreatment
                .Include(p => p.PatientDiagnosis)
                .Include(p => p.Treatment)
                .FirstOrDefaultAsync(m => m.PatientTreatmentId == id);
            if (patientTreatment == null)
            {
                return NotFound();
            }

            return View(patientTreatment);
        }

        // Confirm the user to delete and delete the record. 
        // POST: HKPatientTreatment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientTreatment = await _context.PatientTreatment.FindAsync(id);
            _context.PatientTreatment.Remove(patientTreatment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Indicates whether the record exists when the record modification is in progress
        private bool PatientTreatmentExists(int id)
        {
            return _context.PatientTreatment.Any(e => e.PatientTreatmentId == id);
        }
        //Get diagnos ID from the session
        private int getDID()
        {
            if (HttpContext.Session.GetInt32("DID") == null)
            {
                TempData["Message"] = "No Diagnos.";
                ModelState.AddModelError("", "Select a patient.");
                return -1;
            }

            return (int)HttpContext.Session.GetInt32("DID");
        }
    }
}
