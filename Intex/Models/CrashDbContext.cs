using System;
using Microsoft.EntityFrameworkCore;

namespace Intex.Models
{
    public class CrashDbContext : DbContext
    {
        public CrashDbContext(DbContextOptions<CrashDbContext> options) : base(options)
        {

        }

        public DbSet<Crash> CrashData { get; set; }
    }
}
