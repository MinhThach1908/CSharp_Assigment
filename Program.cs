// See https://aka.ms/new-console-template for more information

using CSharp_Assignment.Menu;

MainMenu menu = new MainMenu();
menu.DisplayMainMenu();


// while (true)
// {
//     Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
//     Console.WriteLine("1. Đăng ký tài khoản.");
//     Console.WriteLine("2. Đăng nhập hệ thống.");
//     Console.WriteLine("3. Thoát.");
//     Console.WriteLine("——————————————————");
//     Console.WriteLine("Nhập lựa chọn của bạn (1,2,3):");
//     int choice = Console.Read();
//     switch (choice)
//     {
//         case 1:
//             Console.WriteLine("Chuc nang dang phat trien");
//             break;
//         case 2:
//             Console.WriteLine("In Development");
//             break;
//         case 3:
//             Console.WriteLine("Tạm biệt. Hẹn gặp lại.");
//             break;
//         default:
//             Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
//             break;
//     }
//     if (choice == 3)
//     {
//         break;
//     }
// }