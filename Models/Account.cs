namespace Bank_Management_System;

public class Account
{
    public long AccountNumber { get; set; }
    public decimal CurrnetBalance { get; set; }
    public AccountStatus AccountStatus {get;set;}= AccountStatus.Active;
    public AccountType AccountType {get;set;}= AccountType.current;
    public DateTime OpeingDate { get; set; }
    public Branch Branch { get; set; } = null!;
    public string BranchCode { get; set; }=null!;
    public ICollection<CustomerAccount> CustomerAccounts {get;set;}= new List<CustomerAccount>();
    public ICollection<Transaction>Transactions {get;set;}=new List<Transaction>();




}
