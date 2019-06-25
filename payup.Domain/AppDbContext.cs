using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace payupApi.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(p => p.Id);
            builder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Employee>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Employee>().HasMany(p => p.Dependents).WithOne(p => p.Employee).HasForeignKey(p => p.EmployeeId);

            builder.Entity<Employee>().HasData
            (
                // Id set manually due to in-memory provider
                new Employee { Id = Guid.NewGuid(), FirstName = "Clark", LastName = "Kent" },
                new Employee { Id = Guid.NewGuid(), FirstName = "Bruce", LastName = "Wayne" },
                new Employee { Id = Guid.NewGuid(), FirstName = "Hal", LastName = "Jordan" },
                new Employee { Id = Guid.NewGuid(), FirstName = "Arthur", LastName = "Curry" }
            );



            builder.Entity<Dependent>().ToTable("Dependents");
            builder.Entity<Dependent>().HasKey(p => p.Id);
            builder.Entity<Dependent>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Dependent>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Dependent>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
        }
    }
}