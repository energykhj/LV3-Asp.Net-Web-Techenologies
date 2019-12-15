/*
 * PROG2230 Assignment2
 * 
 * Purpose: Data persistence, sorting & filtering
 * 
 * Revision History
 *    Oct 3, 2019 - Heuijin Ko Created
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
    public class HKMedicationsController : Controller
    {
        private readonly HKPatientsContext _context;

        public HKMedicationsController(HKPatientsContext context)
        {
            _context = context;
        }

        // Retrieves Medication Information of selected medication type from HKMedications
        public async Task<IActionResult> Index(int mID = 0, string mName = "")
        {
            if(mID != 0 && mName != "")
            {
                HttpContext.Session.SetString("MediTypeID", mID.ToString());
                HttpContext.Session.SetString("MediTypeName", mName);
            }
            else if (mID != 0 && mName == "")
            {
                try
                {
                    var mTypeName = await _context
                                     .MedicationType
                                     .SingleOrDefaultAsync(a => a.MedicationTypeId == mID);

                    if (mTypeName == null)
                    {
                        TempData["Message"] = "Please select a Medication Type to see.";
                        return RedirectToAction("Index", "MTMedicationType");
                    }
                    else
                    {
                        mName = mTypeName.Name;

                        HttpContext.Session.SetString("MediTypeID", mID.ToString());
                        HttpContext.Session.SetString("MediTypeName", mTypeName.Name);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.GetBaseException().Message);
                    return RedirectToAction("Index", "HKMedicationTypes");
                }
            }            
            else
            {
                if (HttpContext.Session.GetString("MediTypeID") == null && 
                        HttpContext.Session.GetString("MediTypeName") == null)
                {
                    TempData["Message"] = "Please select a Medication Type";
                    return RedirectToAction("Index", "HKMedicationTypes");
                }

                else if (HttpContext.Session.GetString("MediTypeID") != null && 
                    HttpContext.Session.GetString("MediTypeName") == null)
                {
                    var mTypeName = await _context
                        .MedicationType
                        .Where(a => a.MedicationTypeId == mID)
                        .FirstOrDefaultAsync();


                    HttpContext.Session.SetString("MediTypeName", mTypeName.Name);
                }

                mID = Convert.ToInt32(HttpContext.Session.GetString("MediTypeID"));
                mName = HttpContext.Session.GetString("MediTypeName");

            }

            try
            {
                var medication = await  _context
                       .Medication
                       .Include(a => a.MedicationType)
                       .Where(a => a.MedicationTypeId == mID)
                       .OrderBy(a => a.Name).ThenBy(a => a.Concentration)
                       .ToListAsync();

                ViewBag.TypeName = mName;

                return View(medication);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Exception in Medication: {ex.GetBaseException().Message}");
                return View(new List<Medication>());
            }

        }

        // GET: Details of Medication
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medication
                .Include(m => m.ConcentrationCodeNavigation)
                .Include(m => m.DispensingCodeNavigation)
                .Include(m => m.MedicationType)
                .FirstOrDefaultAsync(m => m.Din == id);
            if (medication == null)
            {
                return NotFound();
            }

            ViewBag.MedicationTypeName = medication.MedicationType.Name;
            return View(medication);
        }

        // GET: Create view to create a new medication
        public IActionResult Create()
        {
            ViewData["ConcentrationCode"] = new SelectList(_context.ConcentrationUnit.OrderBy(a => a.ConcentrationCode), "ConcentrationCode", "ConcentrationCode");
            ViewData["DispensingCode"] = new SelectList(_context.DispensingUnit.OrderBy(a => a.DispensingCode), "DispensingCode", "DispensingCode");

            ViewBag.MedicationTypeID = HttpContext.Session.GetString("MediTypeID");
            ViewBag.MedicationTypeName = HttpContext.Session.GetString("MediTypeName");

            Medication mediModel = new Medication();
            mediModel.MedicationTypeId = Convert.ToInt32(HttpContext.Session.GetString("MediTypeID"));

            return View(mediModel);
        }

        // POST: Create a new medication
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Din,Name,Image,MedicationTypeId,DispensingCode,Concentration,ConcentrationCode")] Medication medication)
        {
            if (medication.Concentration > 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var duplicate = from aMedi in _context.Medication
                                        where (aMedi.Din == medication.Din ||
                                                (aMedi.Name == medication.Name &&
                                                aMedi.Concentration == medication.Concentration &&
                                                aMedi.ConcentrationCode == medication.ConcentrationCode))
                                        select aMedi;

                        if (duplicate.Any())
                        {
                            TempData["Message"] = "Same data already exists.";
                        }
                        else
                        {
                            _context.Add(medication);
                            await _context.SaveChangesAsync();


                            return RedirectToAction("Index", "HKMedications");

                            //return RedirectToAction(nameof(Index));
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["Message"] = ex.Message;
                        return View(medication);
                    }
                }
            }
            else
            {
                TempData["Message"] = "Concentration must be greater than 0.";
            }

            ViewData["ConcentrationCode"] = new SelectList(_context.ConcentrationUnit.OrderBy(a => a.ConcentrationCode), "ConcentrationCode", "ConcentrationCode", medication.ConcentrationCode);
            ViewData["DispensingCode"] = new SelectList(_context.DispensingUnit.OrderBy(a => a.DispensingCode), "DispensingCode", "DispensingCode", medication.DispensingCode);

            return View(medication);
        }

        // GET: Edit view to edit medication
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medication.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            ViewData["ConcentrationCode"] = new SelectList(_context.ConcentrationUnit.OrderBy(a => a.ConcentrationCode), "ConcentrationCode", "ConcentrationCode", medication.ConcentrationCode);
            ViewData["DispensingCode"] = new SelectList(_context.DispensingUnit.OrderBy(a => a.DispensingCode), "DispensingCode", "DispensingCode", medication.DispensingCode);
            // ViewData["MedicationTypeId"] = new SelectList(_context.MedicationType, "MedicationTypeId", "Name", medication.MedicationTypeId);

            ViewBag.MedicationTypeName = HttpContext.Session.GetString("MediTypeName");

            return View(medication);
        }

        // POST: Edit medication
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Din,Name,Image,MedicationTypeId,DispensingCode,Concentration,ConcentrationCode")] Medication medication)
        {           
            if (id != medication.Din)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationExists(medication.Din))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index", "HKMedications");  
                //RedirectToAction("Index", "HKMedications?");
            }

            ViewData["ConcentrationCode"] = new SelectList(_context.ConcentrationUnit.OrderBy(a => a.ConcentrationCode), "ConcentrationCode", "ConcentrationCode", medication.ConcentrationCode);
            ViewData["DispensingCode"] = new SelectList(_context.DispensingUnit.OrderBy(a => a.DispensingCode), "DispensingCode", "DispensingCode", medication.DispensingCode);
            // ViewData["MedicationTypeId"] = new SelectList(_context.MedicationType, "MedicationTypeId", "Name", medication.MedicationTypeId);

            return View(medication);
        }

        // GET: Delete view to confirm medication
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var medication = await _context.Medication
                        .Include(m => m.ConcentrationCodeNavigation)
                        .Include(m => m.DispensingCodeNavigation)
                        .Include(m => m.MedicationType)
                        .FirstOrDefaultAsync(m => m.Din == id);
                if (medication == null)
                {
                    return NotFound();
                }

                ViewBag.MedicationTypeName = medication.MedicationType.Name;
                return View(medication);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: Delete medication
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var medication = await _context.Medication.FindAsync(id);
                _context.Medication.Remove(medication);
                await _context.SaveChangesAsync();

                TempData["message"] = $"Medication '{medication.Name}' deleted from database";
                return RedirectToAction("Index", "HKMedications");
            }
            catch (Exception ex)
            {
                TempData["message"] = "error on delete: " + ex.GetBaseException().Message;
            }

            return RedirectToAction("Delete", new { ID = id });
        }

        private bool MedicationExists(string id)
        {
            return _context.Medication.Any(e => e.Din == id);
        }
    }
}
