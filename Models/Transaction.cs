namespace Bank_Management_System;

public class Transaction
{
public int TransactionNumber {get;set;}
public DateTime TransactionDate {get;set;}
public decimal Amount {get;set;}
public string? Note {get;set;}
public TransactionType TransactionType{get;set;}=TransactionType.Withdrawal;
public Account Account {get;set;}=null!;
public long AccountNumber {get;set;}
}
