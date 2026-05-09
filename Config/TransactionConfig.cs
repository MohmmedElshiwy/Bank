using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank_Management_System;

public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
       builder.HasOne(t=>t.Account)
       .WithMany(a=>a.Transactions)
       .HasForeignKey(t=>t.AccountNumber);

       builder.HasKey(t=>t.TransactionNumber);

       builder.Property(t=>t.TransactionDate).HasDefaultValueSql("GetDate()");

       builder.Property(t=>t.Amount).HasColumnType("decimal(18,4)");

    }
}
