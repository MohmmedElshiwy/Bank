using Bank_Management_System;
using Microsoft.EntityFrameworkCore;

public class CustomerUi
{
    private readonly AppDbContext context;
    private readonly CustomerHandler customerHandler;

    public CustomerUi(AppDbContext dbContext)
    {
        context = dbContext;
        customerHandler = new CustomerHandler(context);
    }



    #region Add
    public async Task AddCustomer()
    {
        Console.WriteLine("Enter Customer Name ");
        string cuName = Console.ReadLine()!.Trim();

        Console.WriteLine("Enter National ID");
        string cuNationalId = Console.ReadLine()!.Trim();
        while (await context.Customers.AnyAsync(c => c.NationalId == cuNationalId))
        {
            System.Console.WriteLine("this National id Is alredy Exsist \n");
            System.Console.WriteLine("Enter National ID !");
            cuNationalId = Console.ReadLine()!.Trim();

        }
        Console.WriteLine("Enter Date of Birth (yyyy-MM-dd)");
        DateTime cuBirth;

        while (!DateTime.TryParse(Console.ReadLine(), out cuBirth))
        {
            Console.WriteLine("Birth is not valid, try again:");
        }

        Console.WriteLine("Enter Customer Email");
        string? cuEmail = Console.ReadLine()?.Trim();

        Console.WriteLine("Enter Customer Type (1- Individuals / 2- Businesses)");

        CustomerType cuType;
        while (!Enum.TryParse<CustomerType>(Console.ReadLine(), out cuType) ||
               !Enum.IsDefined(typeof(CustomerType), cuType))
        {
            Console.WriteLine("Invalid type, enter 1 or 2:");
        }

        var customer = new Customer
        {
            FullName = cuName,
            NationalId = cuNationalId,
            DateOfBirth = cuBirth,
            Email = cuEmail,
            CustomerType = cuType
        };

        await customerHandler.AddCutmoerAsync(customer);

        System.Console.WriteLine("Customer Added Successfuly ");
    }

    #endregion









}