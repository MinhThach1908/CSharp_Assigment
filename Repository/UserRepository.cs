using System.Data;
using CSharp_Assignment.Entity;
using CSharp_Assignment.Repository.Interface;
using MySqlConnector;

namespace CSharp_Assignment.Repository;

public class UserRepository : IUserRepository
{
    
    private const string MyConnectionString = "server=127.0.0.1;uid=root;" + "pwd=;database=spring_hero_bank";
    
    public Transaction Deposit(Transaction deposit, double amount)
    {
        MySqlTransaction transaction = null;
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            transaction = conn.BeginTransaction();
            var command =
                new MySqlCommand(
                    "update accounts set balance = @amount + balance WHERE account_number = @accountNumber", conn);
            command.Connection = conn;
            command.Parameters.AddWithValue("@amount", amount);
            command.Parameters.AddWithValue("@accountNumber", deposit.AccountNumber);
            command.Transaction = transaction;
            command.ExecuteNonQuery();
            var command1 =
                new MySqlCommand(
                    "insert into transaction(id, account_number, type, created_at, amount, message, status)values (@id, @accountNumber, @type, @createdAt, @amount, @message, @status)");
            command1.Connection = conn;
            command1.Parameters.AddWithValue("@id", deposit.Id);
            command1.Parameters.AddWithValue("@accountNumber", deposit.AccountNumber);
            command1.Parameters.AddWithValue("@type", deposit.Type);
            command1.Parameters.AddWithValue("@createdAt", deposit.CreatedAt);
            command1.Parameters.AddWithValue("@amount", deposit.Amount);
            command1.Parameters.AddWithValue("@message", deposit.Message);
            command1.Parameters.AddWithValue("@status", deposit.Status);
            command1.Transaction = transaction;
            command1.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            transaction.Rollback();
            throw;
        }

        return deposit;
    }

    public Transaction Withdraw(Transaction withdraw, double amount)
    {
        MySqlTransaction transaction = null;
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            transaction = conn.BeginTransaction();
            var command =
                new MySqlCommand(
                    "update accounts set balance = balance - @amount WHERE account_number = @accountNumber and balance >= @amount", conn);
            command.Connection = conn;
            command.Parameters.AddWithValue("@amount", amount);
            command.Parameters.AddWithValue("@accountNumber", withdraw.AccountNumber);
            command.Transaction = transaction;
            int check = command.ExecuteNonQuery();
            if (check == 0)
            {
                Console.WriteLine("Số dư tài khoản không đủ để thực hiện giao dịch");
                transaction.Rollback();
                return withdraw;
            }
            var command1 =
                new MySqlCommand(
                    "insert into transaction(id, account_number, type, created_at, amount, message, status)values (@id, @accountNumber, @type, @createdAt, @amount, @message, @status)");
            command1.Connection = conn;
            command1.Parameters.AddWithValue("@id", withdraw.Id);
            command1.Parameters.AddWithValue("@accountNumber", withdraw.AccountNumber);
            command1.Parameters.AddWithValue("@type", withdraw.Type);
            command1.Parameters.AddWithValue("@createdAt", withdraw.CreatedAt);
            command1.Parameters.AddWithValue("@amount", withdraw.Amount);
            command1.Parameters.AddWithValue("@message", withdraw.Message);
            command1.Parameters.AddWithValue("@status", withdraw.Status);
            command1.Transaction = transaction;
            command1.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            transaction.Rollback();
            throw;
        }

        return withdraw;
    }

    public Transaction Transfer(Transaction transfer, double amount)
    {
        MySqlTransaction transaction = null;
        try
        {
            MySqlConnection conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            transaction = conn.BeginTransaction();
            var command =
                new MySqlCommand(
                    "UPDATE user_account SET balance =balance - @amount  WHERE account_number = @accountNumber and balance >= @amount ", conn);
            command.Connection = conn;
            command.Parameters.AddWithValue("@amount", amount);
            command.Parameters.AddWithValue("@accountNumber", transfer.SenderAccountNumber);
            command.Transaction = transaction;
            int check = command.ExecuteNonQuery();
            if (check == 0)
            {
                Console.WriteLine("Số dư tài khoản không đủ để thực hiện giao dịch");
                transaction.Rollback();
                return transfer;
            }

            var command1 =
                new MySqlCommand(
                    "UPDATE user_account SET balance =balance + @amount  WHERE account_number = @accountNumber and status =1 ", conn);
            command1.Connection = conn;
            command1.Parameters.AddWithValue("@amount", amount);
            command1.Parameters.AddWithValue("@accountNumber", transfer.ReceiverAccountNumber);
            command1.Transaction = transaction;
            command1.ExecuteNonQuery();
            
            var command2 =
                new MySqlCommand(
                    "insert into transaction(id, account_number, type, created_at, amount, sender_account_number, receiver_account_number, message, status)values (@id, @accountNumber, @type, @createdAt, @amount, @senderAccountNumber, @receiverAccountNumber, @message, @status)");
            command2.Connection = conn;
            command2.Parameters.AddWithValue("@id", transfer.Id);
            command2.Parameters.AddWithValue("@accountNumber", transfer.SenderAccountNumber);
            command2.Parameters.AddWithValue("@type", transfer.Type);
            command2.Parameters.AddWithValue("@createdAt", transfer.CreatedAt);
            command2.Parameters.AddWithValue("@amount", transfer.Amount);
            command2.Parameters.AddWithValue("@senderAccountNumber", transfer.SenderAccountNumber);
            command2.Parameters.AddWithValue("@receiverAccountNumber", transfer.ReceiverAccountNumber);
            command2.Parameters.AddWithValue("@message", transfer.Message);
            command2.Parameters.AddWithValue("@status", transfer.Status);
            command2.Transaction = transaction;
            command2.ExecuteNonQuery();
            
            var command3 =
                new MySqlCommand(
                    "insert into transaction(id, account_number, type, created_at, amount, sender_account_number, receiver_account_number, message, status)values (@id, @accountNumber, @type, @createdAt, @amount, @senderAccountNumber, @receiverAccountNumber, @message, @status)");
            command3.Parameters.AddWithValue("@id", transfer.Id);
            command3.Parameters.AddWithValue("@accountNumber", transfer.ReceiverAccountNumber);
            command3.Parameters.AddWithValue("@type", transfer.Type);
            command3.Parameters.AddWithValue("@createdAt", transfer.CreatedAt);
            command3.Parameters.AddWithValue("@amount", transfer.Amount);
            command3.Parameters.AddWithValue("@senderAccountNumber", transfer.SenderAccountNumber);
            command3.Parameters.AddWithValue("@receiverAccountNumber", transfer.ReceiverAccountNumber);
            command3.Parameters.AddWithValue("@message", transfer.Message);
            command3.Parameters.AddWithValue("@status", transfer.Status);
            command3.Transaction = transaction;
            command3.ExecuteNonQuery();
            transaction.Commit();
            conn.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            transaction.Rollback();
            throw;
        }

        return transfer;
    }

    public Account CheckBalance(Account account)
    {
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command = new MySqlCommand("select balance from accounts where account_number = @accountNumber", conn);
            command.Parameters.AddWithValue("@accountNumber", account.AccountNumber);
            command.Connection = conn;
            MySqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                account = new Account();
                account.Balance = dataReader.GetDouble("balance");
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return account;
    }

    public List<Transaction> TransactionHistoryByAccountNumber(string accountNumber)
    {
        List<Transaction> transactions = new List<Transaction>();
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command = new MySqlCommand("select * from transaction where account_number = @accountNumber", conn);
            command.Parameters.AddWithValue("@accountNumber", accountNumber);
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

    public Account Save(Account account)
    {
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command =
                new MySqlCommand(
                    "insert into accounts(account_number, username, password, phone, balance, is_admin, status) values (@accountNumber, @username, @password, @phone, @balance, @isAdmin, @status);");
            command.Connection = conn;
            command.Parameters.AddWithValue("@accountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("@username", account.Username);
            command.Parameters.AddWithValue("@password", account.Password);
            command.Parameters.AddWithValue("@phone", account.Phone);
            command.Parameters.AddWithValue("@balance", account.Balance);
            command.Parameters.AddWithValue("@isAdmin", account.IsAdmin);
            command.Parameters.AddWithValue("@status", account.Status);
            command.ExecuteNonQuery();
            conn.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return account;
    }

    public Account EditInformation(Account account, string accountNumber)
    {
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();
            var command =
                new MySqlCommand(
                    "update accounts set username = @username, phone = @phone, account_number = @accountNumber", conn);
            command.Connection = conn;
            command.Parameters.AddWithValue("@accountNumber", accountNumber);
            command.Parameters.AddWithValue("@username", account.Username);
            command.Parameters.AddWithValue("@phone", account.Phone);
            command.ExecuteNonQuery();
            conn.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return account;
    }

    public Account ChangePassword(Account account, string accountNumber, string password)
    {
        try
        {
            var conn = new MySqlConnection(MyConnectionString);
            conn.Open();

            string hashedPassword = null;

            using (MySqlCommand command =
                   new MySqlCommand("select password from accounts where account_number = @accountNumber", conn))
            {
                command.Parameters.AddWithValue("@accountNumber", accountNumber);
                using (MySqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        hashedPassword = dataReader.GetString("password");
                    }
                }
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            if (passwordMatch)
            {
                using (MySqlCommand command1 = new MySqlCommand("update accounts set password = @newPassword where account_number = @accountNumber", conn))
                {
                    command1.Parameters.AddWithValue("@accountNumber", accountNumber);
                    command1.Parameters.AddWithValue("@password", password);
                    command1.Parameters.AddWithValue("@newPassword", account.Password);
                    command1.ExecuteReader();
                }
            }
            else
            {
                Console.WriteLine("Mật khẩu không hợp lệ, vui lòng thử lại.");
            }
            conn.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return account;
    }
}