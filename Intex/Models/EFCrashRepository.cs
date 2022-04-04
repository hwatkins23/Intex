using System;
using System.Linq;

namespace Intex.Models
{
    public class EFCrashRepository : ICrashRepository
    {
        private CrashDbContext _context { get; set; }

        public EFCrashRepository(CrashDbContext temp)
        {
            _context = temp;
        }

        public IQueryable<Crash> CrashData => _context.CrashData;

        public void SaveCrash(Crash c)
        {
            _context.SaveChanges();
        }

        public void AddCrash(Crash c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }

        //public void EditCrash(Crash c)
        //{
        //_context.Update(c);
        //_context.SaveChanges();

        //return RedirectToAction("ShowBowlers");
        //}

        public void DeleteCrash(Crash c)
        {
            _context.Remove(c);
            _context.SaveChanges();
        }
    }
}
