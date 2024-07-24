using CSharp_Assignment.Entity;

namespace CSharp_Assignment.Repository.Interface;

public interface IUserRepository
{
    Transaction Deposit(Transaction deposit, double amount);
    Transaction Withdraw(Transaction withdraw, double amount);
    Transaction Transfer(Transaction transfer, double amount);
    Account CheckBalance(Account account);
    List<Transaction> TransactionHistoryByAccountNumber(string accountNumber);
    Account Save(Account account);
    Account EditInformation(Account account, string accountNumber);
    Account ChangePassword(Account account, string accountNumber, string password);
    
}