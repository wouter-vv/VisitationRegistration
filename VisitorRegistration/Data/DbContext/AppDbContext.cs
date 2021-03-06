﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisitorRegistration.Data.Entities;
using VisitorRegistration.ViewModels;

namespace VisitorRegistration.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<VisitType> VisitTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }

    }
}
