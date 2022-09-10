using BankApplication.Models.Enums;

namespace BankApplication.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Card> Cards { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Profile> UsersProfiles { get; set; }
    public DbSet<CardSample> CardSamples { get; set; }
    public DbSet<AvatarModel> Files { get; set; }

    public DbSet<Feature> Features { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region HasData
        var feature1 = new Feature() { Id = 1, CardSampleId = 1, Title = "30% cashback", Info = "For your purchases" };
        var feature2 = new Feature() { Id = 2, CardSampleId = 1, Title = "No commission", Info = "Transfers/payment for services" };
        var feature3 = new Feature() { Id = 3, CardSampleId = 1, Title = "Saving", Info = "Сash points for every purchase" };

        var feature4 = new Feature() { Id = 4, CardSampleId = 2, Title = "12% per annum", Info = "Interest rate" };
        var feature5 = new Feature() { Id = 5, CardSampleId = 2, Title = "Up to 55 days", Info = "For any card purchases" };
        var feature6 = new Feature() { Id = 6, CardSampleId = 2, Title = "Up to 70 000$", Info = "Credit limit" };

        var feature7 = new Feature() { Id = 7, CardSampleId = 3, Title = "Cool design", Info = "Interest style for bank card" };
        var feature8 = new Feature() { Id = 8, CardSampleId = 3, Title = "Pay games/films", Info = "15% discount" };
        var feature9 = new Feature() { Id = 9, CardSampleId = 3, Title = "Relax", Info = "The card will do it all" };

        modelBuilder.Entity<Feature>().HasData(feature1, feature2, feature3, 
            feature4, feature5, feature6, feature7, feature8, feature9);
        modelBuilder.Entity<CardSample>().HasData(
            new CardSample()
            {
                Id = 1,
                Name = "Debit card",
                Type = CardType.Debit,                
                ImageUrl = "/img/bank-card.png",
                Info = "The right debit card that makes money. Withdraw cash from all ATMs and earn cash points"
            },

            new CardSample()
            {
                Id = 2,
                Name = "Credit card",
                Type = CardType.Credit,                
                ImageUrl = "/img/credit-card.png",
                Info = "Instant decision, only a passport is needed. Free credit card delivery today"
            },

            new CardSample()
            {
                Id = 3,
                Name = "Special pink card",
                Type = CardType.Special,
                ImageUrl = "/img/special-card.png",
                Info = "Best of the best, you feel me? :)"
            }
        );  
        #endregion
    }
}
