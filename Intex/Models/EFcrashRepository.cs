using System;
using System.Linq;

namespace Intex.Models
{
    public class EFcrashRepository : IcrashRepository
    {
        private crashDbContext _context { get; set; }

        public EFcrashRepository(crashDbContext temp)
        {
            _context = temp;
        }

        public IQueryable<crash> crashes => _context.crashes;

        public void SaveCrash(crash c)
        {
            _context.SaveChanges();
        }

        public void AddCrash(crash c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }


        public void DeleteCrash(crash c)
        {
            _context.Remove(c);
            _context.SaveChanges();
        }
    }
}
