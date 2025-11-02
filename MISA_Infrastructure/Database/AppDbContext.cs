using Microsoft.EntityFrameworkCore;
using MISA_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Database
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<SalariesComposition> SalariesCompositions { get; set; }
        public DbSet<SalariesCompositionSystem> SalariesCompositionSystems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().ToTable("pa_organization");
            modelBuilder.Entity<SalariesComposition>().ToTable("pa_salaries_composition");
            modelBuilder.Entity<SalariesCompositionSystem>().ToTable("pa_salaries_composition_system");
            base.OnModelCreating(modelBuilder);
        }
    }
}
