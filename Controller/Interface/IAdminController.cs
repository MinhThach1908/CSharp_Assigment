using CSharp_Assignment.Entity;

namespace CSharp_Assignment.Controller.Interface;

public interface IAdminController
{
    void UserList();
    void TransactionHistoryList();
    void DisplayPersonalInfo(Account account);
    void SearchUserByName();
    void SearchUserByAccountNumber();
    void SearchUserByPhoneNumber();
    void AddNewUser();
    void LockAndUnlockUserAccount();
    void SearchTransactionHistoryByAccountNumber();
    void ChangeAccountInformation();
    void ChangePassword();
}