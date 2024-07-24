namespace CSharp_Assignment.Controller.Interface;

public interface IUserController
{
    void Deposit(string accountNumber);
    void Withdraw(string accountNumber);
    void Transfer(string accountNumber);
    void CheckBalance(string accountNumber);
    void EditInformation(string accountNumber);
    void ChangePassword(string accountNumber);
    void ViewTransactionHistory(string accountNumber);
    
}