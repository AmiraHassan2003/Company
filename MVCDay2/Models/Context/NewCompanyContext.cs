using System;
using Microsoft.EntityFrameworkCore;
using MVCDay2.Models.Entities;
namespace MVCDay2.Models.Context;

public class NewCompanyContext : DbContext
{
    public NewCompanyContext() : base() { }
    public NewCompanyContext(DbContextOptions options) : base(options) { }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Server=.; Database=NewCompany; Integrated Security=True; Encrypt=True; TrustServerCertificate=True");
    //}

    public DbSet<Company> company { get; set; }
    public DbSet<Employee> employee { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Company>().HasMany(D => D.employees).WithOne(E => E.Company).
        //IsRequired().OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Company>().HasMany(D => D.employees).WithOne(E => E.Company).OnDelete(DeleteBehavior.Cascade);
    }
}

