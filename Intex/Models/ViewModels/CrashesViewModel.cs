using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace Intex.Models.ViewModels
{
    public class CrashesViewModel
    {
        public IQueryable<crash> crashes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
