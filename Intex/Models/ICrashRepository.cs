using System;
using System.Linq;

namespace Intex.Models
{
    public interface ICrashRepository
    {
            public IQueryable<Crash> CrashData { get; }

            public void SaveCrash(Crash c);
            public void AddCrash(Crash c);
            //public void EditCrash(CrashData c);
            public void DeleteCrash(Crash c);

    }
}
