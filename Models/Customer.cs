namespace Bank_Management_System;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public CustomerType CustomerType {get;set;}= CustomerType.individuals;
    public string NationalId { get; set; } = null!;
    public ICollection<CustomerAccount> CustomerAccounts { get; set; } = new List<CustomerAccount>();


}
