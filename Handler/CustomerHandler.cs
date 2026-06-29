using Microsoft.EntityFrameworkCore;

namespace Bank_Management_System;

public class CustomerHandler
{
    private readonly AppDbContext context;

    public CustomerHandler(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<List<Customer>> GetAllAsync()
    {
        var cutomers = await context.Customers.AsNoTracking().ToListAsync();
        return cutomers;
    }




    #region Add
         public async Task AddCutmoerAsync(Customer customer)
    {

        var exists = await context.Customers.AnyAsync(c => c.NationalId == customer.NationalId);

        if (exists)
        {
            
          throw new InvalidOperationException("Customer already exists.");
   
        }
            
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
    }

    #endregion
   



    #region GetBy ID
         public async Task<Customer> GetByIdAsync(int id)
    {
        var customers = await context.Customers.FindAsync(id);

        if (customers is null)
        {
            throw new InvalidOperationException("Customer not found!");
        }
        return customers;

    }

    #endregion
   



   #region Update
       public async Task UpdateCutomerAsync(int id ,Customer customer)
    {
        var exsitingCustomer = await context.Customers.FindAsync(id);
        if (exsitingCustomer is null) throw new InvalidOperationException("Customer Not Found");
        exsitingCustomer.FullName = customer.FullName;
        exsitingCustomer.CustomerType = customer.CustomerType;
        exsitingCustomer.Email = customer.Email;
        exsitingCustomer.NationalId = customer.NationalId;

        await context.SaveChangesAsync();
    }


   #endregion
 




    #region Remove
          public async Task RemoveCustomerAsync(int id)
    {
        var exsist = await context.Customers.FindAsync(id);
        if (exsist is null)
        {
            throw new InvalidOperationException("Customer not found!");
        }
        context.Customers.Remove(exsist);
        await context.SaveChangesAsync();

    }


    #endregion
  


}
