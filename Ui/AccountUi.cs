using Bank_Management_System;
using Microsoft.EntityFrameworkCore;

public class AccountUi
{
    private readonly AppDbContext context;
    private readonly AccountHandler accountHandler;
    private readonly CustomerHandler customerHandler;

    public AccountUi(AppDbContext dbContext)
    {
        context = dbContext;
        accountHandler = new AccountHandler(context);
        customerHandler = new CustomerHandler(context);
    }


    #region Add
    public async Task AddToCustomer()
    {
        var account = await OpenAccount();
        await accountHandler.AddAccountAsync(account);
        var customers = await customerHandler.GetAllAsync();

        for (int i = 0; i < customers.Count; i++)
        {
            System.Console.WriteLine($"{i + 1} -  ID{customers[i].Id}  :  {customers[i].FullName}  ");
        }
        Console.WriteLine("Select Customer Number:");

        int index;
        while (!int.TryParse(Console.ReadLine()!.Trim(), out index) || index < 1 || index > customers.Count)
        {
            Console.WriteLine("Invalid selection, try again:");

        }
        var selectedCustomer = customers[index - 1];
        Console.WriteLine("Choose Account Status:");
        Console.WriteLine("1 - Active");
        Console.WriteLine("2 - Closed");
        AccountStatus accStatus;
        string? input;
        do
        {
            input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                accStatus = AccountStatus.Active;
                break;
            }

        }
        while (!Enum.TryParse<AccountStatus>(input, out accStatus) || !Enum.IsDefined(accStatus));

        Console.WriteLine($"AccountStatus Selected: {accStatus}");


        Console.WriteLine("Choose Owner Ship Typee");
        Console.WriteLine("1 - Holder ");
        Console.WriteLine("2 - Co Holder ");

        OwnerShipType ownerShipType;

        string? ownerInput;

        do
        {
            ownerInput = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(ownerInput))
            {
                ownerShipType = OwnerShipType.Primary_Holder;
                break;
            }
        } while (!Enum.TryParse<OwnerShipType>(ownerInput, out ownerShipType) || !Enum.IsDefined(ownerShipType));
        System.Console.WriteLine($"OwnerShipType Selected : {ownerShipType}");




        await accountHandler.AssignAccountToCustomer(selectedCustomer.Id, account.AccountNumber, ownerShipType, accStatus);

        Console.WriteLine("Account created successfully ✔");


    }


    async Task<Account> OpenAccount()
    {
        System.Console.WriteLine(" ===== Open New Account ===== ");
        long acNumber = await accountHandler.GenerateAccountNumberAsync();
        Console.WriteLine($"Generated Account Number: {acNumber}");


        Console.WriteLine($"Generated Account Number: {acNumber}");
        System.Console.WriteLine("Insert Branch Code ");
        string branchCode = Console.ReadLine()!.Trim();


        while (!await context.Branches.AnyAsync(b => b.Code == branchCode) || string.IsNullOrWhiteSpace(branchCode))
        {
            System.Console.WriteLine("Ivalid Branch Code ");
            branchCode = Console.ReadLine()!.Trim();

        }

        var newaccount = new Account
        {
            AccountNumber = acNumber,
            BranchCode = branchCode,
            OpeingDate = DateTime.Now,
            CurrnetBalance = 0


        };
       
        return newaccount;






    }


    #endregion





    #region Update
    public async Task UpdateAccountState()
    {
        System.Console.WriteLine(" ===== Update Account Status ===== ");

        System.Console.WriteLine("Enter Account Number ");
        long acNumber;


        Account? account = null;
        while (account is null)
        {
            string? input = Console.ReadLine()?.Trim();
            if (!long.TryParse(input, out acNumber))
            {
                Console.WriteLine("Invalid Input, try again");
                continue;
            }
            account = await context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == acNumber);
            if (account == null)
                Console.WriteLine("Account not found");
        }



        var cuAcc = await context.CustomerAccounts.FirstOrDefaultAsync(ca => ca.AccountNumber == account.AccountNumber);
        if (cuAcc is null)
        {
            Console.WriteLine("No customer linked to this account");
            return;
        }
        var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == cuAcc.CustomerId);
        System.Console.WriteLine($"Account Number : {account.AccountNumber}");
        System.Console.WriteLine($"Customer Id : {cuAcc?.CustomerId}");
        System.Console.WriteLine($"Customer Name : {customer?.FullName}");
        Console.WriteLine($"Current Status: {account.AccountStatus}\n");
        Console.WriteLine("Press Enter to keep current status Or choose one of the Next \n");
        Console.WriteLine("1 - Active");
        Console.WriteLine("2 - Closed");

        AccountStatus accountStatus;
        while (true)
        {
            string? statusInput = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(statusInput))
            {
                accountStatus = account.AccountStatus;
                break;
            }
            if (Enum.TryParse<AccountStatus>(statusInput, out accountStatus) && Enum.IsDefined(accountStatus))
            {
                break;
            }
            Console.WriteLine("Invalid input, choose 1 or 2");
        }

        bool updated = await accountHandler.UpdateAccountStatus(
         account.AccountNumber,
         accountStatus
     );

        if (!updated)
        {
            Console.WriteLine("Update failed");
            return;
        }

        account.AccountStatus = accountStatus;

        Console.WriteLine("Account Status Updated Successfully ✔");
        Console.WriteLine($"Status : {account.AccountStatus}");




    }


    #endregion





    #region Remove
    public async Task RemoveAccount()
    {
        System.Console.WriteLine("Enter Account Number To remove");
        long acNumber;
        Account? account = null;
        while (true)
        {
            string input = Console.ReadLine()!.Trim();
            if (!long.TryParse(input, out acNumber) || String.IsNullOrWhiteSpace(input))
            {
                System.Console.WriteLine("Invalid Input Try Again ");
                continue;
            }
            account = await context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == acNumber);
            if (account is null)
            {
                System.Console.WriteLine("Account Not Found");
                continue;
            }
            break;
        }


        var cuAc = await context.CustomerAccounts.FirstOrDefaultAsync(ca => ca.AccountNumber == account.AccountNumber);
        if (cuAc is null)
        {
            Console.WriteLine("No customer linked to this account");
            return;
        }
        var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == cuAc.CustomerId);

        System.Console.WriteLine($"Account Number : {account.AccountNumber}");
        System.Console.WriteLine($"Customer Id : {cuAc.CustomerId}");
        System.Console.WriteLine($"Customer Name : {customer?.FullName}");

        await accountHandler.RemoveAccountAsync(account.AccountNumber);
        System.Console.WriteLine("Account Removed Successfuly");



    }


    #endregion



    #region  GetAll
    public async Task GetAll()
    {
        int count = 1;
        var accounts = await accountHandler.GetAll();
        if (!accounts.Any())
        {
            System.Console.WriteLine("No Account Found");
        }
        foreach (var account in accounts)
        {
            var customers = account.CustomerAccounts
            .Select(ca => new
            {
                Name = ca.Customer.FullName,
                Customer_Type = ca.Customer.CustomerType
            }).ToList();


            System.Console.WriteLine($"{count++} : {string.Join(" | ", customers)}");

            System.Console.WriteLine(
                $"Branch: {account.BranchCode} | " +
                $"Balance: {account.CurrnetBalance} | " +
                $"Type: {account.AccountType} | " +
                $"Status: {account.AccountStatus}"
            );

            System.Console.WriteLine("-----------------------------------");
        }
    }
    #endregion


}