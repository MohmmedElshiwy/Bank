using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank_Management_System;

public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a=>a.AccountNumber);
        builder.Property(a=>a.AccountNumber)
        .ValueGeneratedNever();
        builder.HasOne(a=>a.Branch)
        .WithMany(b=>b.Accounts)
        .HasForeignKey(a=>a.BranchCode);
    }
}
