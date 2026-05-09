using Bank_Management_System;
using Microsoft.EntityFrameworkCore;

public class TransUi
{
    private readonly AppDbContext context;
    private readonly TransactionHandler transactionHandler;



    public TransUi(AppDbContext dbContext)
    {
        context = dbContext;
        transactionHandler = new TransactionHandler(context);
    }

    private long ReadAccountNumber(string message)
    {
        Console.WriteLine(message);

        long number;
        while (!long.TryParse(Console.ReadLine()?.Trim(), out number))
        {
            Console.WriteLine("Invalid input, try again:");
        }

        return number;
    }


    private decimal ReadAmount()
    {
        Console.WriteLine("Enter amount:");

        decimal amount;
        while (!decimal.TryParse(Console.ReadLine()?.Trim(), out amount) || amount <= 0)
        {
            Console.WriteLine("Invalid amount, try again:");
        }

        return amount;
    }

    private (long from, long to, decimal amount) GetTransferInput()
    {
        var from = ReadAccountNumber("Enter Sender Account:");
        var to = ReadAccountNumber("Enter Receiver Account:");
        var amount = ReadAmount();

        return (from, to, amount);
    }


    public async Task Deposit()
    {
        var acc = ReadAccountNumber("Enter Account Number");
        var amount = ReadAmount();

        await transactionHandler.DepositAsync(acc, amount);
        System.Console.WriteLine($"The amount has been added {amount} successfully");
    }
    public async Task Withdraw()
    {

        var acc = ReadAccountNumber("Enter Account Number");
        var amount = ReadAmount();
        await transactionHandler.WithdrawAsync(acc, amount);
        System.Console.WriteLine($"An amount has been deducted {amount}");

    }

    public async Task Transfer()
    {
        var data = GetTransferInput();

        var result = await transactionHandler.TransferAsync(data.from, data.to, data.amount);
        if (!result)
        {
            Console.WriteLine("Transfer failed ❌ ");
            return;
        }
        var sender = await context.CustomerAccounts.Where(cu=>cu.AccountNumber==data.from).Select(cu=>cu.Customer.FullName).FirstOrDefaultAsync();
        var recivr = await context.CustomerAccounts.Where(cu=>cu.AccountNumber==data.to).Select(cu=>cu.Customer.FullName).FirstOrDefaultAsync();

        Console.WriteLine("Transfer Successfuly ✔ \n ");
        System.Console.WriteLine("===================================");
        System.Console.WriteLine($"== Transferred from {sender} ");
        System.Console.WriteLine($"== Transferred to {recivr}     ");
        System.Console.WriteLine($"== Amount transferred {data.amount}");
        System.Console.WriteLine("=================================");
    }

    public async Task Payment()
    {

        var acc = ReadAccountNumber("Enter Account Number");
        var amount = ReadAmount();
        var result = await transactionHandler.PaymentAsync(acc, amount);
        if (!result)
        {
            Console.WriteLine("Payment failed ❌ (insufficient balance or invalid account)");
            return;
        }
        System.Console.WriteLine($"Buy Internet ");
        System.Console.WriteLine($"Account Number {acc}");
        System.Console.WriteLine($"SAR : {amount}");
        System.Console.WriteLine($"Date : {DateTime.Now}");



    }

}