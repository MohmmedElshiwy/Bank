using Bank_Management_System;
using Microsoft.EntityFrameworkCore;
public class AccountHandler
{
    private readonly AppDbContext context;

    public AccountHandler(AppDbContext dbContext)
    {
        context = dbContext;
    }



    #region GetAll Accounts

    public async Task<List<Account>> GetAll()
    {
        var accounts = await context.Accounts
            .Include(a => a.CustomerAccounts)
            .ThenInclude(ca => ca.Customer)
            .ToListAsync();

        return accounts;
    }
    #endregion


    #region Add
    public async Task AddAccountAsync(Account account)
    {
        if (await context.Accounts.AnyAsync(a => a.AccountNumber == account.AccountNumber))
        {
            throw new Exception("Account already exists");
        }

        context.Accounts.Add(account);
        await context.SaveChangesAsync();

    }

    public async Task<bool> AssignAccountToCustomer(
        int customerId,
         long accountNumber,
          OwnerShipType ownershipType,
           AccountStatus accountStatus)
    {
        var cuExists = await context.Customers.AnyAsync(c => c.Id == customerId);
        if (!cuExists)
        {
            return false;
        }
        var acExists = await context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        if (acExists == null)
        {
            return false;
        }

        bool relationExists = await context.CustomerAccounts.AnyAsync(ca =>
          ca.CustomerId == customerId &&
          ca.AccountNumber == accountNumber);

        if (relationExists)
            return false;


        var customerAccount = new CustomerAccount
        {
            CustomerId = customerId,
            AccountNumber = accountNumber,
            OwnerShipStartDate = DateTime.Now,
            OwnerShipType = ownershipType,
            AccountStatus = accountStatus


        };
        context.CustomerAccounts.Add(customerAccount);
        await context.SaveChangesAsync();
        return true;

    }



    #endregion




    #region Update
    public async Task<bool> UpdateAccountStatus(long accountNumber, AccountStatus status)
    {
        var acExists = await context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        if (acExists == null)
        {
            return false;
        }
        acExists.AccountStatus = status;
        await context.SaveChangesAsync();
        return true;

    }
    #endregion




    #region Remove
    public async Task<bool> RemoveAccountAsync(long accountNumber)
    {
        var existingAccount = await context.Accounts.SingleOrDefaultAsync(a => a.AccountNumber == accountNumber);
        if (existingAccount is null)
        {
            return false;
        }
        var cuAc = await context.CustomerAccounts.Where(ca => ca.AccountNumber == existingAccount.AccountNumber).ToListAsync();
        if (!cuAc.Any())
        {
            return false;
        }
        context.CustomerAccounts.RemoveRange(cuAc);
        context.Accounts.Remove(existingAccount);
        await context.SaveChangesAsync();
        return true;
    }

  public async Task<long> GenerateAccountNumberAsync()
{
    long accountNumber;
    bool exists;

    do
    {
        string datePart = DateTime.Now.ToString("yyMMddHHmm"); 
        int randomPart = Random.Shared.Next(1000, 9999);

        string fullNumber = datePart + randomPart;
        accountNumber = long.Parse(fullNumber);

        exists = await context.Accounts
            .AnyAsync(a => a.AccountNumber == accountNumber);

    } while (exists);

    return accountNumber;
}

    #endregion


}
