using CSharp_Assignment.Entity;
using CSharp_Assignment.Repository.Interface;
using MySqlConnector;

namespace CSharp_Assignment.Repository;

public class AdminRepository : IAdminRepository
{
    private const string MyConnectionString = "server=127.0.0.1;uid=root;" + "pwd=;database=spring_hero_bank";
    
    public List<Account> FindAll()
    {
        var result = new List<Account>();
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command = new MySqlCommand("select * from accounts where status != -1");
            command.Connection = conn;
            MySqlDataReader dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                var accountNumber = dataReader.GetString("account_number");
                var username = dataReader.GetString("username");
                var password = dataReader.GetString("password");
                var phone = dataReader.GetString("phone");
                var balance = dataReader.GetDouble("balance");
                var isAdmin = dataReader.GetBoolean("is_admin");
                var status = dataReader.GetInt32("status");
                var user = new Account();
                user.AccountNumber = accountNumber;
                user.Username = username;
                user.Password = password;
                user.Phone = phone;
                user.Balance = balance;
                user.IsAdmin = isAdmin;
                user.Status = status;
                result.Add(user);
            }
            conn.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return result;
    }
    
    public Account FindByUsername(string username)
    {
        Account user = null;
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command = new MySqlCommand("select * from accounts where {username} = @username");
            command.Parameters.AddWithValue("@username", username);
            command.Connection = conn;
            MySqlDataReader dataReader = command.ExecuteReader();
            if(dataReader.Read())
            {
                user = new Account();
                user.AccountNumber = dataReader.GetString("account_number");
                user.Username = dataReader.GetString("username");
                user.Password = dataReader.GetString("password");
                user.Phone = dataReader.GetString("phone");
                user.Balance = dataReader.GetDouble("balance");
                user.IsAdmin = dataReader.GetBoolean("is_admin");
                user.Status = dataReader.GetInt32("status");
                conn.Close();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return user;
    }

    public Account FindByAccountNumber(string accountNumber)
    {
        Account user = null;
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command = new MySqlCommand("select * from accounts where {account_number} = @accountNumber");
            command.Parameters.AddWithValue("@accountNumber", accountNumber);
            command.Connection = conn;
            MySqlDataReader dataReader = command.ExecuteReader();
            if(dataReader.Read())
            {
                user = new Account();
                user.AccountNumber = dataReader.GetString("account_number");
                user.Username = dataReader.GetString("username");
                user.Password = dataReader.GetString("password");
                user.Phone = dataReader.GetString("phone");
                user.Balance = dataReader.GetDouble("balance");
                user.IsAdmin = dataReader.GetBoolean("is_admin");
                user.Status = dataReader.GetInt32("status");
                conn.Close();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return user;
    }

    public Account FindByPhone(string phone)
    {
        Account user = null;
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command = new MySqlCommand("select * from accounts where {phone} = @phone");
            command.Parameters.AddWithValue("@phone", phone);
            command.Connection = conn;
            MySqlDataReader dataReader = command.ExecuteReader();
            if(dataReader.Read())
            {
                user = new Account();
                user.AccountNumber = dataReader.GetString("account_number");
                user.Username = dataReader.GetString("username");
                user.Password = dataReader.GetString("password");
                user.Phone = dataReader.GetString("phone");
                user.Balance = dataReader.GetDouble("balance");
                user.IsAdmin = dataReader.GetBoolean("is_admin");
                user.Status = dataReader.GetInt32("status");
                conn.Close();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return user;
    }

    public void LockOrUnlock(string accountNumber, int choice)
    {
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            if (choice == 1)
            {
                var command = new MySqlCommand("update accounts set status = 0 where account_number = @accountNumber",
                    conn);
                command.Parameters.AddWithValue("@accountNumber", accountNumber);
                command.ExecuteNonQuery();
                Console.WriteLine("Đang khóa tài khoản...");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Khóa thành công.");
            } else if (choice == 2)
            {
                var command = new MySqlCommand("update accounts set status = 1 where account_number = @accountNumber",
                    conn);
                command.Parameters.AddWithValue("@accountNumber", accountNumber);
                command.ExecuteNonQuery();
                Console.WriteLine("Đang mở khóa tài khoản...");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Mở khóa thành công.");
            }
            conn.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    public List<Transaction> TransactionHistory()
    {
        List<Transaction> transactions = new List<Transaction>();

        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command = new MySqlCommand("select * from transaction", conn);
            command.Connection = conn;
            MySqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Transaction transaction = new Transaction();
                transaction.Id = dataReader.GetInt64("id");
                transaction.AccountNumber = dataReader.GetString("account_number");
                transaction.Type = dataReader.GetInt32("type");
                transaction.CreatedAt = dataReader.GetDateTime("created_at");
                transaction.Amount = dataReader.GetDouble("amount");
                transaction.SenderAccountNumber = dataReader.GetString("sender_account_number");
                transaction.ReceiverAccountNumber = dataReader.GetString("receiver_account_number");
                transaction.Message = dataReader.GetString("message");
                transaction.Status = dataReader.GetInt32("status");
                transactions.Add(transaction);
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return transactions;
    }
}