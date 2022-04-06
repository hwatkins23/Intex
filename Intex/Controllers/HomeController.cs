﻿using Intex.Models;
using Intex.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ML.OnnxRuntime;
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

        public IActionResult Summary(int pageNum = 1)
        {
            int pageSize = 100;

            var x = new CrashesViewModel
            {
                crashes = repo.crashes
                .OrderBy(x => x.CRASH_DATE)
                .Skip((pageNum = 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = repo.crashes.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
        };

            return View(x);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
