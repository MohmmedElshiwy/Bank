using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank_Management_System;

public class BranchConfig : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasKey(b=>b.Code);
        builder.HasOne(b => b.Manager)
        .WithOne(m => m.Branch)
        .HasForeignKey<Manager>(m => m.BranchCode);

        builder.OwnsOne(b => b.Address, address =>
        {
            address.Property(a => a.City)
                .HasColumnName("City")
                .HasMaxLength(100);

            address.Property(a => a.Street)
               .HasColumnName("Street")
               .HasMaxLength(150);

            address.Property(a => a.Building)
               .HasColumnName("Building")
               .HasMaxLength(50);

        });
        
        
    }
}
