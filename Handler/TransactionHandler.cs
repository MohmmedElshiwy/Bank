using Bank_Management_System;
using Microsoft.EntityFrameworkCore;

public class TransactionHandler
{

    private readonly AppDbContext context;
    public TransactionHandler(AppDbContext dbContext)
    {
        context= dbContext;
    }


    #region Deposit
    public async Task<bool> DepositAsync(long accountNumber, decimal amount)
    {
        if (amount <= 0) return false;

        var account = await context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        if (account == null) return false;

        account.CurrnetBalance += amount;

        context.Transactions.Add(new Transaction
        {
            AccountNumber = accountNumber,
            Amount = amount,
            TransactionType = TransactionType.Deposit
        });

        await context.SaveChangesAsync();
        return true;
    }

    #endregion





    #region  Transfer
    public async Task<bool> TransferAsync(
long fromAccountNumber,
long toAccountNumber,
decimal amount)
    {
        if (amount <= 0) return false;

        var fromAccount = await context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == fromAccountNumber);

        var toAccount = await context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == toAccountNumber);

        if (fromAccount == null || toAccount == null)
            return false;

        if (fromAccount.CurrnetBalance < amount)
            return false;

        fromAccount.CurrnetBalance -= amount;
        toAccount.CurrnetBalance += amount;

        context.Transactions.Add(new Transaction
        {
            AccountNumber = fromAccountNumber,
            Amount = amount,
            TransactionType = TransactionType.Transfer
        });

        await context.SaveChangesAsync();
        return true;
    }

    #endregion





    #region Debit
    private async Task<bool> DebitAsync(
long accountNumber,
decimal amount,
TransactionType type)
    {
        if (amount <= 0) return false;

        var account = await context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

        if (account == null) return false;

        if (account.CurrnetBalance < amount) return false;

        account.CurrnetBalance -= amount;

        context.Transactions.Add(new Transaction
        {
            AccountNumber = accountNumber,
            Amount = amount,
            TransactionType = type
        });

        await context.SaveChangesAsync();
        return true;
    }

    #endregion




    #region Withdraw
    public async Task<bool> WithdrawAsync(long accountNumber, decimal amount)
    {
        return await DebitAsync(
            accountNumber,
            amount,
            TransactionType.Withdrawal
        );
    }

    #endregion




    #region Payment
    public async Task<bool> PaymentAsync(long accountNumber, decimal amount)
    {
        return await DebitAsync(
            accountNumber,
            amount,
            TransactionType.Payment
        );
    }



    #endregion


}