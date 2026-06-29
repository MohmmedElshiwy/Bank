using Bank_Management_System;

public class CustomerAccount
{
    public int CustomerId {get;set;}
    public Customer Customer {get;set;}=null!;
    public long AccountNumber {get;set;}
    public Account Account {get;set;}=null!;
    public OwnerShipType OwnerShipType {get;set;}=OwnerShipType.Primary_Holder;
    public AccountStatus AccountStatus {get;set;}=AccountStatus.Active;
    public DateTime OwnerShipStartDate {get;set;}
}
