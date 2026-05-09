using Bank_Management_System;
var context = new AppDbContext();
var accountUi = new AccountUi(context);
var customerUi = new CustomerUi(context);
var transUi = new TransUi(context);

//  var branch = new Branch
//  {
//      Code ="Ca_001",
//      Name = "Cairo Branch",
//      PhoneNumber = "020123456789987"

//  };
//  await context.Branches.AddAsync(branch);
//  await context.SaveChangesAsync();
//  var manager = new Manager
//  {

//      BranchCode = branch.Code,
//      FullName = "Toji",
//      Email = "TOJI@gmail.com",
//      PhoneNumber = "0201004132115"

//  };
//  await context.Managers.AddAsync(manager);


//  await context.SaveChangesAsync();

System.Console.WriteLine("Welcome To Bank Management System ");
while (true)
{
    System.Console.WriteLine("==================================");
    System.Console.WriteLine("  National Bank - Management      ");
    System.Console.WriteLine("==================================");
    System.Console.WriteLine("1 - Add New Customer");
    System.Console.WriteLine("2 - Open a New Account For Customer");
    System.Console.WriteLine("3 - Transactions                    ");
    System.Console.WriteLine("4 - Update Account Status (Active / Closed)");
    System.Console.WriteLine("5 - Remove an Account From Customer");
    System.Console.WriteLine("6 - List All Customer (With accounts)");
    System.Console.WriteLine("0 - Exit ");

    var choice = Console.ReadLine()!.Trim();
    switch (choice)
    {
        case "1":
            await customerUi.AddCustomer();
            break;
        case "2":
            await accountUi.AddToCustomer();
            break;
        case "3":
            System.Console.WriteLine("1) Deposit ");
            System.Console.WriteLine("2) Withdraw");
            System.Console.WriteLine("3) Transfer");
            System.Console.WriteLine("4) Payment");
            System.Console.WriteLine("0) Return");
            string input = Console.ReadLine()!.Trim();
            switch (input)
            {
                case "1":
                await transUi.Deposit();
                break;
                case "2":
                await transUi.Withdraw();
                break;
                case "3":
                await transUi.Transfer();
                break;
                case"4":
                await transUi.Payment();
                break;
                case"0":
                    System.Console.WriteLine("Going to Menu ......");
                break;
                default:
                System.Console.WriteLine("Invalid Input");
                break;
            }
        break;

        case "4":
            await accountUi.UpdateAccountState();
            break;
            case"5":
             await accountUi.RemoveAccount();
             break;
        case "6":
            await accountUi.GetAll();
            break;
        case "0":
            System.Console.WriteLine("GodBy");
            return;
        default:
            System.Console.WriteLine("Invalid Input");
            break;
    }
}








