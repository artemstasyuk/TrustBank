namespace BankApplication.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserByIdAsync(int id) => await _dbContext.Users.FindAsync(new object[] {id});
    public async Task<User> GetUserByEmailAsync(string email) => 
        await _dbContext.Users.Where(p => p.Email == email).FirstOrDefaultAsync();
    public async Task CreateUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await SaveAsync();
    }
    public async Task<bool> UserExist(string email) =>
        await _dbContext.Users.AnyAsync(x => x.Email == email);
    

    public bool CheckPassword(string passwordLogin, User user ) =>
        passwordLogin.Equals(user.Password);
    public async Task SaveAsync() => 
        await _dbContext.SaveChangesAsync();
}