using CSharp_Assignment.Entity;

namespace CSharp_Assignment.Repository.Interface;

public interface IAdminRepository
{
    
    List<Account> FindAll();
    Account FindByUsername(string username);
    Account FindByAccountNumber(string accountNumber);
    Account FindByPhone(string phone);
    void LockOrUnlock(string accountNumber, int choice);
    List<Transaction> TransactionHistory();

}