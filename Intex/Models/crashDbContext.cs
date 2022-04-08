using System;
using Microsoft.EntityFrameworkCore;

namespace Intex.Models
{
    //------------------------------------------------Crash Database Context-----------------------------------------------------
    public class crashDbContext : DbContext
    {
        public crashDbContext(DbContextOptions<crashDbContext> options) : base(options)
        {

        }

        public DbSet<crash> crashes { get; set; }
    }
}
