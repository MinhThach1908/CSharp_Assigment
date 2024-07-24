namespace CSharp_Assignment.Entity;

public class Transaction
{
    public long Id { get; set; }
    
    public string AccountNumber { get; set; }
    
    public int Type { get; set; } // 1. withdraw | 2. deposit | 3. transfer
    
    public DateTime CreatedAt { get; set; }
    
    public double Amount { get; set; }
    
    public string SenderAccountNumber { get; set; }
    
    public string ReceiverAccountNumber { get; set; }
    
    public string Message { get; set; }
    
    public int Status { get; set; } // 1. error | 2. success
}