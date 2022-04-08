using System;
using System.Linq;

namespace Intex.Models.ViewModels
{
    public class CrashesViewModel
    {
        public IQueryable<crash> crashes { get; set; }
        public PageInfo PageInfo { get; set; }
        public int Severity { get; set; }
        public string CountyName { get; set; }
        public string CityString { get; set; }
    }
}
