using CSharp_Assignment.Controller.Interface;
using CSharp_Assignment.Repository;

using CSharp_Assignment.Entity;

namespace CSharp_Assignment.Controller;

public class UserController : IUserController
{
    private UserRepository _userRepository = new UserRepository();
    private PasswordSecurity.PasswordSecurity _passwordSecurity = new PasswordSecurity.PasswordSecurity();
    private LoginRepository _loginRepository = new LoginRepository();
    
    public void Deposit(string accountNumber)
    {
        Transaction transaction = new Transaction();
        transaction.AccountNumber = accountNumber;
        Console.WriteLine("Số tiền gửi vào: ");
        double transactionAmount = double.Parse(Console.ReadLine());
        Console.WriteLine("Số tài khoản đã được gửi: " + transactionAmount);
        Console.WriteLine("Nhập nội dung: ");
        transaction.Message = Console.ReadLine();
        transaction.CreatedAt = DateTime.Now;
        _userRepository.Deposit(transaction, transactionAmount);
    }

    public void Withdraw(string accountNumber)
    {
        Transaction transaction = new Transaction();
        Console.WriteLine("Số tiền rút ra: ");
        double transactionAmount = double.Parse(Console.ReadLine());
        Console.WriteLine("Số tài khoản đã được rút: " + transactionAmount);
        Console.WriteLine("Nhập nội dung: ");
        transaction.Message = Console.ReadLine();
        transaction.CreatedAt = DateTime.Now;
        _userRepository.Withdraw(transaction, transactionAmount);
    }

    public void Transfer(string accountNumber)
    {
        Transaction transaction = new Transaction();
        Console.WriteLine("Số tài khoản của người gửi");
        transaction.SenderAccountNumber = Console.ReadLine();
        Console.WriteLine("Số tài khoản của người nhận");
        transaction.ReceiverAccountNumber = Console.ReadLine();
        Console.WriteLine("Số tiền giao dịch: ");
        double transactionAmount = double.Parse(Console.ReadLine());
        Console.WriteLine("Số tài khoản  " + transaction.SenderAccountNumber + " đã gửi " + transactionAmount + " đến số tài khoản " + transaction.ReceiverAccountNumber);
        Console.WriteLine("Số tài khoản  " + transaction.ReceiverAccountNumber + " đã nhận " + transactionAmount + " từ số tài khoản " + transaction.SenderAccountNumber);
        Console.WriteLine("Nhập nội dung: ");
        transaction.Message = Console.ReadLine();
        transaction.CreatedAt = DateTime.Now;
        _userRepository.Transfer(transaction, transactionAmount);
    }

    public void CheckBalance(string accountNumber)
    {
        Account account = new Account();
        account.AccountNumber = accountNumber;
        account = _userRepository.CheckBalance(account);
        Console.WriteLine("Số dư của tài khoản là: " + account);
    }

    public void EditInformation(string accountNumber)
    {
        Account account = new Account();
        Console.WriteLine("Nhập username: ");
        account.Username = Console.ReadLine();
        Console.WriteLine("Nhập số điện thoại: ");
        account.Phone = Console.ReadLine();
        _userRepository.EditInformation(account, accountNumber);
    }

    public void ChangePassword(string accountNumber)
    {
        Account account = new Account();
        Console.WriteLine("Nhập mật khẩu cũ: ");
        string password = Console.ReadLine();
        _loginRepository.CheckAccount(accountNumber, password);
        Console.WriteLine("Nhập mật khẩu mới: ");
        string newPassword = Console.ReadLine();
        string encryptPassword = _passwordSecurity.EncryptPassword(newPassword);
        Console.WriteLine("Nhập lại mật khẩu: ");
        string checkPassword = Console.ReadLine();
        bool check = _passwordSecurity.DecryptPassword(checkPassword, encryptPassword);
        while (check != true)
        {
            Console.WriteLine("Mật khẩu không đúng, vui lòng thử lại");
            checkPassword = Console.ReadLine();
            check = _passwordSecurity.DecryptPassword(checkPassword, encryptPassword);
        }

        account.Password = encryptPassword;

        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, salt);
        _userRepository.ChangePassword(account, accountNumber, password);
    }

    public void ViewTransactionHistory(string accountNumber)
    {
        List<Transaction> transactions = new List<Transaction>();
        _userRepository.TransactionHistoryByAccountNumber(accountNumber);
        Console.WriteLine("{0,-30} | {1,-30} | {2,-30} | {3,-30} | {4,-30} | {5,-30} | {6,-30} | {7;-30} | {8,-30}","Id", "Số tài khoản", "Kiểu", "Ngày tạo", "Số lượng",
            "Số tài khoản người gửi", "Số tài khoản người nhận", "Nội dung", "Trạng thái");
        foreach (var transaction in transactions)
        {
            Console.WriteLine("{0,-30} | {1,-30} | {2,-30} | {3,-30} | {4,-30} | {5,-30} | {6,-30} | {7;-30} | {8,-30}", transaction.Id,
                transaction.AccountNumber, transaction.Type, transaction.CreatedAt, transaction.Amount, transaction.SenderAccountNumber, transaction.ReceiverAccountNumber, transaction.Message, transaction.Status);
        }
    }
}