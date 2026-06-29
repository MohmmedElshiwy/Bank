using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank_Management_System;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c=>c.FullName).HasMaxLength(100);
        builder.Property(c=>c.NationalId).IsUnicode().HasMaxLength(14);
    }
}
