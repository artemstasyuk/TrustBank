namespace BankApplication.Data.Interfaces;

public interface IAccountRepository
{
    Task<Account> GetAccountByIdAsync(int id);
    Task<bool> AccountExist(string email);
    bool CheckPassword(string passwordLogin, Account account);
    Task<Account> GetAccountByPhoneNumber(string phoneNumber);
    Task<Account> GetAccountByEmail(string email);
    Task CreateAccount(Account account);
    Task VerifyEmail(Account account);
    Task ReturnAccountAsync(int accountId);
    Task DeleteAccount(int accountId);
    Task SaveAsync();
}