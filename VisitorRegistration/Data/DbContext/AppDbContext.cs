using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VisitorRegistration.Data.Entities;

namespace VisitorRegistration.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<VisitType> VisitTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            SeedVisitTypes(modelBuilder);

        }

        private void SeedVisitTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisitType>().HasData(new List<VisitType>
            {
                new VisitType
                {
                    Id = 1,
                    Type = "Bezoek"
                },
                new VisitType
                {
                    Id = 2,
                    Type = "Sollicitatie"
                },
                new VisitType
                {
                    Id = 3,
                    Type = "Levering"
                },
            });
        }
    }
}
