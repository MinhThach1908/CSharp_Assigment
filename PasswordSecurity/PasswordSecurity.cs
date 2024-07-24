namespace CSharp_Assignment.PasswordSecurity;

public class PasswordSecurity
{
    public bool DecryptPassword(string password, string hashedPassword)
    {
        bool check = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        return check;
    }

    public string EncryptPassword(string password)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        return hashedPassword;
    }
}