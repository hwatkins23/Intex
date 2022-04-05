using System;
using Microsoft.EntityFrameworkCore;

namespace Intex.Models
{
    public class crashDbContext : DbContext
    {
        public crashDbContext(DbContextOptions<crashDbContext> options) : base(options)
        {

        }

        public DbSet<crash> crashes { get; set; }
    }
}
