
namespace BankApplication.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IProfileRepository _profileRepository;
    private readonly AppDbContext _dbContext;
    public UserRepository(AppDbContext dbContext, IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
        _dbContext = dbContext;
    }

    public async Task<User> GetUserByIdAsync(int id) => await _dbContext.Users.FindAsync(new object[] {id});

    public async Task<User> GetVerifiedUserByEmailAsync(string email) =>
        await _dbContext.Users.Where(u => u.IsVerified == true && u.Email.Equals(email)).FirstOrDefaultAsync();
    public async Task<User> GetUserByEmailAsync(string email) => 
        await _dbContext.Users.Where(p => p.Email == email).FirstOrDefaultAsync();
    
    public async Task CreateUser(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.IsVerified = false;
        await _dbContext.Users.AddAsync(user);
        await SaveAsync();
    }

    public async Task<bool> UserExist(string email) =>
        await CheckUserStatus(email);

    private async Task<bool> CheckUserStatus(string email)
    {
        var user = await GetVerifiedUserByEmailAsync(email);
        if (user is null) return true;
        return false;
    }

    public async Task<bool> CheckUserCredentials(string email, string password)
    {
        var user =  await GetVerifiedUserByEmailAsync(email);
        if (user is null) return false;
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) return false;
        return true;
    }
    
    public async Task SaveAsync() => 
        await _dbContext.SaveChangesAsync();
}