using Intex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Controllers
{
    public class HomeController : Controller
    {
        //uses the repo to make testing easier
        private IcrashRepository repo;

        public HomeController(IcrashRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Summary()
        {
            var crash = repo.crashes
                .ToList();

            return View(crash);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
