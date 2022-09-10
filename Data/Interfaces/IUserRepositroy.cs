namespace BankApplication.Data.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(int id);
    Task CreateUser(User user);
    Task<bool> UserExist(string email);
    Task<bool> CheckUserCredentials(string email, string password);

}