using CSharp_Assignment.Entity;
using CSharp_Assignment.Repository.Interface;
using MySqlConnector;

namespace CSharp_Assignment.Repository;

public class LoginRepository : ILoginRepository
{
    
    private const string MyConnectionString = "server=127.0.0.1;uid=root;" + "pwd=;database=spring_hero_bank";
    
    public Account CheckAccount(string username, string password)
    {
        Account user = null;
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command = new MySqlCommand("select * from accounts where username = @username", conn);
            command.Parameters.AddWithValue("@username", username);
            command.Connection = conn;
            MySqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                string enteredPassword = dataReader.GetString("password");
                bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, enteredPassword);
                if (passwordMatch)
                {
                    user = new Account();
                    user.AccountNumber = dataReader.GetString("account_number");
                    user.Username = dataReader.GetString("username");
                    user.Password = dataReader.GetString("password");
                    user.Status = dataReader.GetInt32("status");
                }
                else
                {
                    Console.WriteLine("Mật khẩu không đúng. Vui lòng thử lại.");
                }
            }
            conn.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return user;
    }
}