using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank_Management_System;

public class ManagerConfig : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
     builder.Property(m=>m.PhoneNumber).HasMaxLength(15);
     builder.Property(m=>m.HireDate).HasDefaultValueSql ("GETDATE()");
     builder.Property(m=>m.FullName).HasMaxLength(100);
     builder.HasIndex(m => m.BranchCode).IsUnique();
    }
}
