using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HKPatients.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HKPatients.Controllers
{
    /// <summary>
    /// User Role Manintenance, Role allow to only Administrators
    /// </summary>
    [Authorize(Roles = "administrators")]
    public class HKRoleController : Controller
    {
        private readonly HKPatientsContext _context;
        
        public HKRoleController(HKPatientsContext context)
        {
            _context = context;
        }
        /// <summary>
        /// view to list all current roles, ordered by role name
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var hKRole = _context
                           .AspNetRoles
                           .OrderBy(p => p.Name);
            return View(await hKRole.ToListAsync());
        }

        // POST: HKPatients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create an action to add a new role 
        /// </summary>
        /// <param name="aspNetRoles"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, ConcurrencyStamp, NormalizedName")] AspNetRoles aspNetRoles)
        {
            if((aspNetRoles.Name + "").Trim() == "")
            {
                ModelState.AddModelError("", "No role to add");
                TempData["Message"] = "Role name cannot be null, empty, or blanks";
                return RedirectToAction(nameof(Index));
            }

            var dup = _context.AspNetRoles.Where(w => w.Name == aspNetRoles.Name).FirstOrDefault();
            if(dup != null)
            {
                ModelState.AddModelError("", "duplication");
                TempData["Message"] = "Role name ["+ aspNetRoles.Name + "] is already on file";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var roleList = await _context
                           .AspNetRoles
                           .OrderBy(m => m.Id)
                           .LastOrDefaultAsync();

                int roleId = int.Parse(roleList.Id) + 1;
                aspNetRoles.Id = roleId.ToString();
                aspNetRoles.Name = aspNetRoles.Name.Trim();
                aspNetRoles.NormalizedName = aspNetRoles.Name.ToUpper();

                if (ModelState.IsValid)
                {
                    _context.Add(aspNetRoles);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "role added: " + aspNetRoles.Name;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                //   throw;
                ModelState.AddModelError("", ex.GetBaseException().Message);
                TempData["Message"] = "Failed add role: " + aspNetRoles.Name;
                //return View(aspNetRoles);
                return View(nameof(Index));
            }
            return View(nameof(Index));
        }
        /// <summary>
        /// List all users in the given role
        /// </summary>
        /// <param name="id">RoleId</param>
        /// <returns></returns>
        public async Task<IActionResult> UserInRole(string id)
        {
            string roleID = "";
            
            if ((id + "").Trim() != "")
            {
                HttpContext.Session.SetString("RoleId", id);
            }
            else
            {
                roleID = HttpContext.Session.GetString("RoleId");
                if ((roleID + "") == "")
                {
                    TempData["Message"] = "No role to see";
                    return View("Index");
                }
                id = roleID;
            }

            var roleName = _context.AspNetRoles.Where(w => w.Id == id).FirstOrDefault();
            if(roleName != null)
                HttpContext.Session.SetString("RoleName", roleName.Name);

            var userInRole = await (from userIn in _context.AspNetUsers
                                    where _context.AspNetUserRoles.Any(es => (es.UserId == userIn.Id) && (es.RoleId == id))
                                    select userIn).OrderBy(o => o.UserName).ToListAsync();

            var userNotInRole = (from userNotIn in _context.AspNetUsers
                                 where !_context.AspNetUserRoles.Any(es => es.UserId == userNotIn.Id && (es.RoleId == id))
                                 select userNotIn).OrderBy(o => o.UserName).ToList();

            // search user in the role
            ViewData["UserNotInRole"] = new SelectList(userNotInRole, "Id", "UserName");
            ViewData["RoleName"] = (roleName != null)? roleName.Name : "";
            ViewData["RoleId"] = id;

            return View("UserInRole", userInRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddtoRole([Bind("UserId, RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if ((aspNetUserRoles.UserId + "").Trim() == "" && (aspNetUserRoles.RoleId + "").Trim() == "")
            {
                ModelState.AddModelError("", "No User to add");
                TempData["Message"] = "No User to add";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var UserName = _context.AspNetUsers.Where(w => w.Id == aspNetUserRoles.UserId).FirstOrDefault().UserName;
                if (ModelState.IsValid)
                {
                    _context.Add(aspNetUserRoles);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "User added to Role: " + UserName;
                    return RedirectToAction(nameof(UserInRole));
                }
            }
            catch (Exception ex)
            {
                //   throw;
                ModelState.AddModelError("", ex.GetBaseException().Message);
                return View("UserInRole");
            }
            return View("UserInRole");
        }

        // POST: HKPatients/Delete/5   
        /// <summary>
        /// Remove individual users from the role
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveUser(string rId, string id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "No User Role to remove");
                TempData["Message"] = "No User Role to remove";
                return View("UserInRole");
            }
            try
            {
                var role = await _context.AspNetUserRoles.Where(w => w.UserId == id && w.RoleId == rId).SingleAsync();
                _context.AspNetUserRoles.Remove(role);
                await _context.SaveChangesAsync();

                var UserName = _context.AspNetUsers.Where(w => w.Id == id).FirstOrDefault().UserName;
                TempData["Message"] = "User removed in the role: " + UserName;
                return RedirectToAction(nameof(UserInRole));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.GetBaseException().Message);
                return View("UserInRole");
            }
        }

        // GET: HKPatients/Delete/5
        /// <summary>
        /// Delete Role from AspNetRoles
        /// </summary>
        /// <param name="id">RoleId</param>
        /// <returns></returns>
        /// [HttpPost]
        public async Task<IActionResult> DeleteRole(string id, string roleName)
        {
            if (id == null)
            {
                TempData["Message"] = $"No Role to delete";
                return View("Index", _context.AspNetRoles);
            }

            if (id == "1" || roleName == "administrators")
            {
                TempData["Message"] = $"administrators cannot be deleted";
                return View("Index", _context.AspNetRoles);
            }

           var userInRole = await (from userIn in _context.AspNetUsers
                                    where _context.AspNetUserRoles.Any(es => (es.UserId == userIn.Id) && (es.RoleId == id))
                                    select userIn).OrderBy(o => o.UserName).ToListAsync();

            ViewData["RoleId"] = id;
            ViewData["RoleName"] = roleName; // HttpContext.Session.GetString("RoleName");
            var roleId = await _context.AspNetRoles.FindAsync(id);
           
            if (roleId == null)
            {
                TempData["Message"] = $"Invaild user role";
                return View("Index", _context.AspNetRoles);
            }

            return View("DeleteRole", userInRole); 
        }

        // POST: HKPatients/Delete/5
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoleConfimed(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "No User Role to remove");
                TempData["Message"] = "No User Role to remove";
                return View("UserInRole");
            }
            try
            {   
                var results = from c in _context.AspNetUserRoles
                              where c.RoleId == id
                              select c;

                foreach (var userRole in results)
                {
                    _context.AspNetUserRoles.Remove(userRole);
                }
                _context.SaveChanges();
                
                string roleName = _context.AspNetRoles.Where(w => w.Id == id).FirstOrDefault().Name;
                var role = await _context.AspNetRoles.FindAsync(id);
                _context.AspNetRoles.Remove(role);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Role removed: " + roleName;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.GetBaseException().Message);
                return View("UserInRole");
            }
        }        
    }
}