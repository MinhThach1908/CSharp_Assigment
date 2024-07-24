using CSharp_Assignment.Controller.Interface;
using CSharp_Assignment.Entity;
using CSharp_Assignment.Repository;

namespace CSharp_Assignment.Controller;

public class AdminController : IAdminController
{

    private AdminRepository _adminRepository = new AdminRepository();
    private LoginRepository _loginRepository = new LoginRepository();
    private PasswordSecurity.PasswordSecurity _passwordSecurity = new PasswordSecurity.PasswordSecurity();
    private UserRepository _userRepository = new UserRepository();
    
    public void UserList()
    {
        List<Account> accounts = _adminRepository.FindAll();
        Console.WriteLine("{0, -15} | {1, -30} | {2, -30} | {3, -30} | {4, -30} | {5, -30} | {6, -30}  ",
            "Số tài khoản", "Tên tài khoản", "Mật khẩu", "Số điện thoại", "Số dư", "Có phải Admin", "Trạng thái");
        foreach (var user in accounts)
        {
            Console.WriteLine("{0, -15} | {1, -30} | {2, -30} | {3, -30} | {4, -30} | {5, -30} | {6, -30}  ",
                user.AccountNumber, user.Username, user.Password, user.Phone, user.Balance, user.IsAdmin, user.Status);
        }
    }

    public void TransactionHistoryList()
    {
        List<Transaction> transactions = _adminRepository.TransactionHistory();
        Console.WriteLine("{0,-30} | {1,-30} | {2,-30} | {3,-30} | {4,-30} | {5,-30} | {6,-30} | {7;-30} | {8,-30}","Id", "Số tài khoản", "Kiểu", "Ngày tạo", "Số lượng",
            "Số tài khoản người gửi", "Số tài khoản người nhận", "Nội dung", "Trạng thái");
        foreach (var transaction in transactions)
        {
            Console.WriteLine("{0,-30} | {1,-30} | {2,-30} | {3,-30} | {4,-30} | {5,-30} | {6,-30} | {7;-30} | {8,-30}", transaction.Id,
                transaction.AccountNumber, transaction.Type, transaction.CreatedAt, transaction.Amount, transaction.SenderAccountNumber, transaction.ReceiverAccountNumber, transaction.Message, transaction.Status);
        }
    }

    public void DisplayPersonalInfo(Account account)
    {
        Console.WriteLine("{0, -15} ||  {1, -15} || {2, -15} || {3, -15} || {4, -15} || {5, -15} || {6, -15}  ",
            "Số tài khoản", "Tên tài khoản", "Mật khẩu", "Số Điện Thoại", "Số dư", "Có phải Admin", "Trạng thái");
        Console.WriteLine("{0, -15} || {1, -15} || {2, -15} || {3, -15} || {4, -15} || {5, -15} || {6, -15}  ",
            account.AccountNumber, account.Password, account.Phone, account.Balance, account.IsAdmin, account.Status);
    }
    
    public void SearchUserByName()
    {
        Console.WriteLine("Vui lòng nhập tên tài khoản của tài khoản cần tìm: ");
        string username = Console.ReadLine();
        Account account = _adminRepository.FindByUsername(username);
        DisplayPersonalInfo(account);
    }

    public void SearchUserByAccountNumber()
    {
        Console.WriteLine("Vui lòng nhập số tài khoản của tài khoản cần tìm: ");
        string accountNumber = Console.ReadLine();
        Account account = _adminRepository.FindByAccountNumber(accountNumber);
        DisplayPersonalInfo(account);
    }

    public void SearchUserByPhoneNumber()
    {
        Console.WriteLine("Vui lòng nhập số điện thoại của tài khoản cần tìm: ");
        string phone = Console.ReadLine();
        Account account = _adminRepository.FindByPhone(phone);
        DisplayPersonalInfo(account);
    }

    public void AddNewUser()
    {
        Account account = new Account();
        Console.WriteLine("Vui lòng nhập thông tin tài khoản:");
        Console.WriteLine("Nhập số tài khoản: ");
        account.AccountNumber = Console.ReadLine();
        Console.WriteLine("Nhập tên tài khoản: ");
        account.Username = Console.ReadLine();
        Console.WriteLine("Nhập mật khẩu: ");
        string password = Console.ReadLine();
        string encryptPassword = _passwordSecurity.EncryptPassword(password);
        Console.WriteLine("Nhập lại mật khẩu: ");
        string checkPassword = Console.ReadLine();
        bool check = _passwordSecurity.DecryptPassword(checkPassword, encryptPassword);
        while (check != true)
        {
            Console.WriteLine("Nhập mật khẩu sai vui lòng nhập lại: ");
            checkPassword = Console.ReadLine();
            check = _passwordSecurity.DecryptPassword(checkPassword, encryptPassword);
        }

        account.Password = encryptPassword;
        Console.WriteLine("Nhập Số điện thoại: ");
        account.Phone = Console.ReadLine();

        Console.WriteLine("Bạn muốn tạo tài khoản người dùng(1) hay tài khoản admin(2)?");
        int x = int.Parse(Console.ReadLine());
        if (x == 1)
        {
            account.Status = 1;
            Console.WriteLine("Tạo thành công tài khoản người dùng");
        }
        else if (x == 2)
        {
            account.Status = -1;
            Console.WriteLine("Tạo thành công tài khoản admin");
        }
        else
        {
            Console.WriteLine("Lựa chọn không hợp lệ, vui lòng thử lại.");
        }

        _userRepository.Save(account);
    }

    public void LockAndUnlockUserAccount()
    {
        Console.WriteLine("Vui lòng nhập số tài khoản muốn (mở)khóa: ");
        string accountNumber = Console.ReadLine();
        Console.WriteLine("Bạn muốn khóa tài khoản (1) hay mở tài khoản(2): ");
        int x = int.Parse(Console.ReadLine());
        if (x != 1 && x != 2)
        {
            Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại(1 hoặc 2): ");
            x = int.Parse(Console.ReadLine());
        }

        _adminRepository.LockOrUnlock(accountNumber, x);
    }

    public void SearchTransactionHistoryByAccountNumber()
    {
        Console.WriteLine("Vui lòng nhập số tài khoản");
        string accountBank = Console.ReadLine();
        List<Transaction> transactions = _userRepository.TransactionHistoryByAccountNumber(accountBank);
        Console.WriteLine("{0,-30} | {1,-30} | {2,-30} | {3,-30} | {4,-30} | {5,-30} | {6,-30} | {7;-30} | {8,-30}","Id", "Số tài khoản", "Kiểu", "Ngày tạo", "Số lượng",
            "Số tài khoản người gửi", "Số tài khoản người nhận", "Nội dung", "Trạng thái");
        foreach (var transaction in transactions)
        {
            Console.WriteLine("{0,-30} | {1,-30} | {2,-30} | {3,-30} | {4,-30} | {5,-30} | {6,-30} | {7;-30} | {8,-30}", transaction.Id,
                transaction.AccountNumber, transaction.Type, transaction.CreatedAt, transaction.Amount, transaction.SenderAccountNumber, transaction.ReceiverAccountNumber, transaction.Message, transaction.Status);
        }
    }

    public void ChangeAccountInformation()
    {
        Account account = new Account();
        Console.WriteLine("Vui lòng nhập số tài khoản bạn muốn thay đổi");
        string accountNumber = Console.ReadLine();
        Console.WriteLine("Nhập tên tài khoản: ");
        account.Username = Console.ReadLine();
        Console.WriteLine("Nhập số điện thoại:");
        account.Phone = Console.ReadLine();

        _userRepository.EditInformation(account, accountNumber);
    }

    public void ChangePassword()
    {
        Account account = new Account();
        Console.WriteLine("Vui lòng nhập số tài khoản bạn muốn thay đổi mật khẩu: ");
        string accountNumber = Console.ReadLine();
        Console.WriteLine("Nhập tên tài khoản: ");
        string username = Console.ReadLine();
        Console.WriteLine("Nhập mật khẩu cũ: ");
        string password = Console.ReadLine();
        _loginRepository.CheckAccount(username, password);
        Console.WriteLine("Nhập mật khẩu mới: ");
        string newPassword = Console.ReadLine();
        string encryptPassword = _passwordSecurity.EncryptPassword(newPassword);
        Console.WriteLine("Nhập lại mật khẩu: ");
        string checkPassword = Console.ReadLine();
        bool check = _passwordSecurity.DecryptPassword(checkPassword, encryptPassword);
        while (check != true)
        {
            Console.WriteLine("Mật khẩu không đúng, vui lòng nhập lại:");
            checkPassword = Console.ReadLine();
            check = _passwordSecurity.DecryptPassword(checkPassword, encryptPassword);
        }

        account.Password = encryptPassword;

        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, salt);
        _userRepository.ChangePassword(account, accountNumber, password);
    }
}