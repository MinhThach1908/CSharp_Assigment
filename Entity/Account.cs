namespace CSharp_Assignment.Entity;

public class Account
{
    public string AccountNumber { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string Phone { get; set; }
    
    public double Balance { get; set; }
    
    public bool IsAdmin { get; set; } // 0. user | 1. admin
    
    public int Status { get; set; } // 0. lock | 1. unlock
}