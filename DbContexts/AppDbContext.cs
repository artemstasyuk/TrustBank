namespace BankApplication.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<Account> Accounts { get; set; }

}