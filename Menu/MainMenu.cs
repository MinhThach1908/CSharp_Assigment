using System.Data.SqlTypes;
using CSharp_Assignment.Controller;
using CSharp_Assignment.Entity;
using CSharp_Assignment.Repository;

namespace CSharp_Assignment.Menu;

public class MainMenu
{

    public void DisplayMainMenu()
    {
        AdminController adminController = new AdminController();
        AdminMenu adminMenu = new AdminMenu();
        UserMenu userMenu = new UserMenu();
        LoginRepository loginRepository = new LoginRepository();
        Account account = new Account();
        bool exit = true;
        while (exit)
        {
            Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
            Console.WriteLine("1. Đăng ký tài khoản.");
            Console.WriteLine("2. Đăng nhập hệ thống.");
            Console.WriteLine("3.  Thoát.");
            Console.WriteLine("——————————————————");
            Console.WriteLine("Nhập lựa chọn của bạn (1,2,3): ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    adminController.AddNewUser();
                    break;
                case 2:
                    exit = false;
                    string username = null;
                    Console.WriteLine("Vui lòng nhập tên tài khoản: ");
                    username = Console.ReadLine();
                    Console.WriteLine("Nhập mật khẩu: ");
                    string password = Console.ReadLine();
                    account = loginRepository.CheckAccount(username, password);
                    if (account != null)
                    {
                        if (account.Status == 1)
                        {
                            userMenu.DisplayUserMenu(account.AccountNumber, account.Username);
                        }
                        else if (account.Status == -1)
                        {
                            adminMenu.DisplayAdminMenu(account.Username);
                        }
                        else if (account.Status == 0)
                        {
                            Console.WriteLine("Tài khoản này đã bị khóa.");
                        }
                        else
                        {
                            Console.WriteLine("Tài khoản không hợp lệ, vui lòng thử lại.");
                        }
                    }
                    break;
                case 3:
                    exit = false;
                    Console.WriteLine("Tạm biệt. Hẹn gặp lại.");
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                    break;
            }
        }
    }
}
