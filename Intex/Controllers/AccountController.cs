using Intex.Models;
using Intex.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Intex.Controllers
{
//--------------------------------------------AUTHORIZATION-------------------------------------------------------------
    public class AccountController : Controller
    {
        //uses the repo to make testing easier
        private IcrashRepository repo;

        public AccountController(IcrashRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

//--------------------------------------------SUMMMARY FOR ADMIN-------------------------------------------------------------
        public IActionResult AdminSummary(int pageNum = 1)
        {

//--------------------------------------------PAGINATION---------------------------------------------------------------------
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



//--------------------------------------------PASS DATA-------------------------------------------------------------
            var x = new CrashesViewModel
            {
                crashes = repo.crashes
                .OrderByDescending(x => x.CRASH_ID)
                .Skip((pageNum = 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = repo.crashes.Count(),
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

//--------------------------------------------DETAILS PAGE-------------------------------------------------------------
        [HttpGet]
        public IActionResult AdminDetails(int crashID)
        {
            var crash = repo.crashes.Single(x => x.CRASH_ID == crashID);

            return View(crash);
        }


//--------------------------------------------ADD-------------------------------------------------------------
        [HttpGet]

        public IActionResult AddAccident()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAccident(crash c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            c.CRASH_ID = (repo.crashes.Max(c => c.CRASH_ID)) + 1;

            repo.AddCrash(c);
            return View("AdminDetails", c);
        }

//--------------------------------------------EDIT-------------------------------------------------------------
        [HttpGet]
        public IActionResult Edit(int crashID)
        {

            var crash = repo.crashes.Single(x => x.CRASH_ID == crashID);

            return View("AddAccident", crash);
        }

        [HttpPost]
        public IActionResult Edit(crash c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            repo.SaveCrash(c);
            return RedirectToAction("AdminSummary", c);
        }


//--------------------------------------------DELETE-------------------------------------------------------------
        [HttpGet]

        public IActionResult Delete (int crashId)
        {
            var crash = repo.crashes.Single(x => x.CRASH_ID == crashId);
            return View(crash);
        }

        [HttpPost]
        public IActionResult Delete (crash c)
        {
            repo.DeleteCrash(c);
            return RedirectToAction("AdminSummary");
            
            
        }

    }
}
