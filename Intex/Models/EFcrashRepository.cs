using System;
using System.Linq;

namespace Intex.Models
{
    public class EFcrashRepository : IcrashRepository
    {
        private crashDbContext context { get; set; }

        public EFcrashRepository(crashDbContext temp)
        {
            context = temp;
        }

        public IQueryable<crash> crashes => context.crashes;

        public void SaveCrash(crash c)
        {

            context.Update(c);
            context.SaveChanges();
        }

        public void AddCrash(crash c)
        {
            context.Add(c);
            context.SaveChanges();
        }


        public void DeleteCrash(crash c)
        {
            context.Remove(c);
            context.SaveChanges();
        }
    }
}
