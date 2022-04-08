using System;
using System.Linq;

namespace Intex.Models
{
    //---------------------------------------------------------Interface for Crash Repository-------------------------------------------------------
    public interface IcrashRepository
    {
            public IQueryable<crash> crashes { get; }

            public void SaveCrash(crash c);
            public void AddCrash(crash c);
            public void DeleteCrash(crash c);

    }
}
