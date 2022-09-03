using System.Security.Cryptography;
using System.Text;

namespace BankApplication.Data.Repositories;


public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _dbContext;
    public AccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account> GetAccountByIdAsync(int id) =>
        await _dbContext.Accounts.FindAsync(new object[] {id});

    public async Task<Account> GetAccountByPhoneNumber(string phoneNumber) =>
        await _dbContext.Accounts.FindAsync(new object[] {phoneNumber});

    public async Task<Account> GetAccountByEmail(string email) => 
         await _dbContext.Accounts.Where(acc => acc.Email == email).FirstOrDefaultAsync();

    public async Task<bool> AccountExist(string email) =>
        await _dbContext.Accounts.AnyAsync(x => x.Email == email);
    

    public bool CheckPassword(string passwordLogin, Account account) =>
        passwordLogin.Equals(account.Password);
    public async Task VerifyEmail(Account account)
    {
        account.IsVerified = true;
        await SaveAsync();
    }


    public async Task CreateAccount(Account account)
    {
        account.AccountStatus = AccountStatus.Active;
        account.Password = account.Password;  //hashing func
        await _dbContext.Accounts.AddAsync(account);
        await SaveAsync();
    }
    public async Task ReturnAccountAsync(int accountId)
    {
        var account = await _dbContext.Accounts.FindAsync(new object[] {accountId});
        account.AccountStatus = AccountStatus.Returned;
        await SaveAsync();
    }

    public async Task DeleteAccount(int accountId)
    {
        var account = await _dbContext.Accounts.FindAsync(new object[] {accountId});
        account.AccountStatus = AccountStatus.Deleted;
        await SaveAsync();
    }
    public async Task SaveAsync() => 
        await _dbContext.SaveChangesAsync();

    #region Helpers
    
    public string GetHash(string data)
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(data);
        byte[] hash = md5.ComputeHash(inputBytes);
        // step 2, convert byte array to hex string
        var sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

    #endregion
}