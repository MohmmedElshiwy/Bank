namespace Bank_Management_System;

public class Manager
{
    public int Id {get;set;}
    public string FullName {get;set;}=null!;
    public string? Email {get;set;}
    public string PhoneNumber {get;set;}=null!;
    public DateTime HireDate {get;set;}
    public Branch Branch {get;set;} =null!;
    public string BranchCode {get;set;}=null!;
    

}
