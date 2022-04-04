using System;
using System.Linq;

namespace Intex.Models
{
    public interface ICrashRepository
    {
            public IQueryable<Crash> Crashes { get; }

            public void SaveCrash(Crash c);
            public void AddCrash(Crash c);
            //public void EditCrash(Crash c);
            public void DeleteCrash(Crash c);

    }
}
