using CSharp_Assignment.Controller;

namespace CSharp_Assignment.Menu;

public class UserMenu
{
    public void DisplayUserMenu(string accountNumber, string name)
    {
        UserController userController = new UserController();
        bool userExit = true;
        while (userExit) 
        {
            Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
            Console.WriteLine("Chào mừng {0} quay trở lại. Vui lòng chọn thao tác.", name);
            Console.WriteLine("1. Gửi tiền.");
            Console.WriteLine("2. Rút Tiền.");
            Console.WriteLine("3. Chuyển khoản.");
            Console.WriteLine("4. Truy vấn số dư.");
            Console.WriteLine("5. Thay đổi thông tin cá nhân.");
            Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
            Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
            Console.WriteLine("8. Thoát.");
            Console.WriteLine("——————————————————-");
            Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 8):");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    // gửi tiền
                    userController.Deposit(accountNumber);
                    break;
                case 2:
                    // rút tiền
                    userController.Withdraw(accountNumber);
                    break;
                case 3:
                    // chuyển khoản
                    userController.Transfer(accountNumber);
                    break;
                case 4:
                    // truy vấn số dư
                    userController.CheckBalance(accountNumber);
                    break; 
                case 5:
                    // thay đổi thông tin cá nhaan
                    userController.EditInformation(accountNumber);
                    break;
                case 6:
                    // thay đổi mật khẩu
                    userController.ChangePassword(accountNumber);
                    break;
                case 7:
                    // ls gd
                    userController.ViewTransactionHistory(accountNumber);
                    break;
                case 8:
                    userExit = false;
                    Console.WriteLine("Tạm biệt. Hẹn gặp lại.");
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                    break;
            }
        }
    }
}