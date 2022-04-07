using Intex.Models;
using Intex.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
//--------------------------------------------SET UP-------------------------------------------------------------
        private IcrashRepository repo;

        public HomeController(IcrashRepository temp)
        {
            repo = temp;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Summary(int severity, string countyName, string searchString, int pageNum = 1)
        {

            ViewBag.Counties = repo.crashes.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            ViewBag.CountyName = countyName;


//--------------------------------------------PAGINATION-------------------------------------------------------------
            int pageSize = 50;
            int maxPages = 10;

            int v = (int)Math.Ceiling((double)(repo.crashes.Count()) / pageSize);

            int totalPages = v;
            int startPage, endPage;
            if (totalPages <= maxPages)
            {
                // total pages less than max so show all pages
                startPage = 1;
                endPage = totalPages;
            }
            else
            {
                // total pages more than max so calculate start and end pages
                var maxPagesBeforeCurrentPage = (int)Math.Floor((decimal)maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling((decimal)maxPages / (decimal)2) - 1;
                if (pageNum <= maxPagesBeforeCurrentPage)
                {
                    // current page near the start
                    startPage = 1;
                    endPage = maxPages;
                }
                else if (pageNum + maxPagesAfterCurrentPage >= totalPages)
                {
                    // current page near the end
                    startPage = totalPages - maxPages + 1;
                    endPage = totalPages;
                }
                else
                {
                    // current page somewhere in the middle
                    startPage = pageNum - maxPagesBeforeCurrentPage;
                    endPage = pageNum + maxPagesAfterCurrentPage;
                }
            }
            // calculate start and end item indexes
            var startIndex = (pageNum - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize - 1, pageSize - 1);
            // create an array of pages that can be looped over
            var pages = Enumerable.Range(startPage, (endPage + 1) - startPage);
            // update object instance with all pager properties required by the view

            ViewBag.Crash = severity;
            int numRecords = 0;

            if (severity == 0 && countyName == null)
            {
                numRecords = repo.crashes.Count();
            }
            else if (severity != 0 && countyName == null)
            {
                numRecords = repo.crashes.Where(x => x.CRASH_SEVERITY_ID == severity).Count();
            }
            else if (severity == 0 && countyName != null)
            {
                numRecords = repo.crashes.Where(x => x.COUNTY_NAME == countyName).Count();
            }
            else if (severity != 0 && countyName != null)
            {
                numRecords = repo.crashes.Where(x => x.COUNTY_NAME == countyName).Where(x => x.CRASH_SEVERITY_ID == severity).Count();
            }

            ViewBag.CityName = searchString;

            IQueryable<string> countyQuery = from c in repo.crashes orderby c.COUNTY_NAME select c.COUNTY_NAME;

            var crashes = from c in repo.crashes select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                crashes = crashes.Where(x => x.CITY.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(countyName))
            {
                crashes = crashes.Where(x => x.COUNTY_NAME == countyName);
            }
            else
            {
                crashes = repo.crashes
                    .OrderByDescending(x => x.CRASH_ID)
                    .Where(x => x.CRASH_SEVERITY_ID == severity || severity == 0).Where(x => x.COUNTY_NAME == countyName || countyName == null)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize);
            }

            //-----------------------------------------SUMMARY DISPLAY-------------------------------------------------------------
            var x = new CrashesViewModel
            {
                crashes = crashes,

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = crashes.Count(),
                    
                    //(severity == 0
                    //        ? repo.crashes.Count()
                    //        : repo.crashes.Where(x => x.CRASH_SEVERITY_ID == severity).Count()),

                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum,

                    StartPage = startPage,
                    EndPage = endPage,
                    StartIndex = startIndex,
                    EndIndex = endIndex,
                    Pages = pages,

                }
            };

                return View(x);
        }

//--------------------------------------------Details Page-------------------------------------------------------------
        [HttpGet]
        public IActionResult Details(int crashID)
        {
            var crash = repo.crashes.Single(x => x.CRASH_ID == crashID);

            return View(crash);
        }


//--------------------------------------------PRIVACY POLICY-------------------------------------------------------------
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
