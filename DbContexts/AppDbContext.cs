using BankApplication.Models;

namespace BankApplication.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Card> Cards { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<Profile> UsersProfiles { get; set; }
    public DbSet<CardSample> CardSamples { get; set; }
    public DbSet<Feature> Features { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var feature1 = new Feature() { Id = 1, CardSampleId = 1, Title = "30% cashback", Info = "For your purchases" };
        var feature2 = new Feature() { Id = 2, CardSampleId = 1, Title = "No commission", Info = "Transfers/payment for services" };
        var feature3 = new Feature() { Id = 3, CardSampleId = 1, Title = "Saving", Info = "Сash points for every purchase" };

        modelBuilder.Entity<Feature>().HasData(feature1, feature2, feature3);
        modelBuilder.Entity<CardSample>().HasData(
            new CardSample() { 
                Id = 1, 
                Name = "Debit card",                  
                Type = CardType.Debit }
            );
    }
}
