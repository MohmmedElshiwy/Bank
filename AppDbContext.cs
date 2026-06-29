using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Bank_Management_System;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "server=.;database=Bank_Management_System;Trusted_Connection=True;TrustServerCertificate=True;"
        );
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Branch> Branches { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<CustomerAccount> CustomerAccounts { get; set; }
}
