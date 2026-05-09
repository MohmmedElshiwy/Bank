namespace Bank_Management_System;


public class Branch
{
    public string Code { get; set; }=null!;
    public string Name { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Manager Manager { get; set; } = null!;
    public ICollection<Account> Accounts { get; set; } = new List<Account>();


}
