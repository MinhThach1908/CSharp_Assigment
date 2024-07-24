using CSharp_Assignment.Entity;

namespace CSharp_Assignment.Repository.Interface;

public interface ILoginRepository
{
    
    Account CheckAccount(string username, string password);
    
}