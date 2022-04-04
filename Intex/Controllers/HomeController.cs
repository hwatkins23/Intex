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
        //now uses the repo to make testing easier
        private ICrashRepository _repo;

        public HomeController(ICrashRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
