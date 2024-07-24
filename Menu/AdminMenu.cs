using CSharp_Assignment.Controller;

namespace CSharp_Assignment.Menu;

public class AdminMenu
{
    public void DisplayAdminMenu(string name)
    {
        AdminController adminController = new AdminController();
        bool adminExit = true;
        while (adminExit) 
        {
            Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
            Console.WriteLine("Chào mừng Admin {0} quay trở lại. Vui lòng chọn thao tác.", name);
            Console.WriteLine("1. Danh sách người dùng.");
            Console.WriteLine("2. Danh sách lịch sử giao dịch.");
            Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
            Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản.");
            Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại.");
            Console.WriteLine("6. Thêm người dùng mới.");
            Console.WriteLine("7. Khoá và mở tài khoản người dùng.");
            Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản.");
            Console.WriteLine("9. Thay đổi thông tin tài khoản.");
            Console.WriteLine("10. Thay đổi thông tin mật khẩu.");
            Console.WriteLine("11. Thoát.");
            Console.WriteLine("——————————————————-");
            Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 11):");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    adminController.UserList();
                    break;
                case 2:
                    adminController.TransactionHistoryList();
                    break;
                case 3:
                    adminController.SearchUserByName();
                    break;
                case 4:
                    adminController.SearchUserByAccountNumber();
                    break; 
                case 5:
                    adminController.SearchUserByPhoneNumber();
                    break;
                case 6:
                    adminController.AddNewUser();
                    break;
                case 7:
                    adminController.LockAndUnlockUserAccount();
                    break;
                case 8:
                    adminController.SearchTransactionHistoryByAccountNumber();
                    break;
                case 9:
                    adminController.ChangeAccountInformation();
                    break;
                case 10:
                    adminController.ChangePassword();
                    break;
                case 11:
                    adminExit = false;
                    Console.WriteLine("Tạm biệt. Hẹn gặp lại.");
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                    break;
            }
        }
    }
}