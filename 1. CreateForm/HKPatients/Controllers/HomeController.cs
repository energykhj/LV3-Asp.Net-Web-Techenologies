using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HKPatients.Models;

namespace HKPatients.Controllers
{
    // display introduction of assginment1 and creator
    // Heuijin Ko(8187452) Sept 13, 2019
    public class HomeController : Controller
    {
        // default action
        public IActionResult Index()
        {
            return View();
        }

        // when click the label of about
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        // when click the label of Contact
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        // when click the label of Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
