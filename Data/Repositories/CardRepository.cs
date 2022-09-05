using System.Text;

namespace BankApplication.Data.Repositories;


public class CardRepository : ICardRepository
{
    private readonly AppDbContext _appDbContext;
    public CardRepository(AppDbContext dbContext)
    {
        _appDbContext = dbContext;
    }
    
    public async Task CreateCardAsync(Card card, int profileId)
    {
        card.CardStatus = CardStatus.Active;
        card.CardNumber = GenerateCardNumber();
        card.CVV = GenerateCvvCode();
        card.ProfileId = profileId;
        await _appDbContext.Cards.AddAsync(card);
        await SaveAsync();
    }
    
    
    public async Task ReturnCardAsync(int cardId)
    {
        var card = await _appDbContext.Cards.FindAsync(new object[] {cardId});
        if(card is null) return;
        card.CardStatus = CardStatus.Returned;
        await SaveAsync();
    }
    
    
    public async Task DeleteCardAsync(int cardId)
    {
        var card = await _appDbContext.Cards.FindAsync(new object[] {cardId});
        if(card is null) return;
        
        card.CardStatus = CardStatus.Deleted;
        await SaveAsync();
    }

    public async Task UpdateCardInitialsAsync(Card card)
    {
        var cardDb = await _appDbContext.Cards.FindAsync(new object[] {card.Id});
        if (cardDb is null) return;
        
        cardDb.CardName = card.CardName;
        cardDb.CardSurname = card.CardSurname;
        await SaveAsync();
    }
    

    public async Task<List<Card>> GetAllCardsByProfileIdAsync(int profileId) =>
        await _appDbContext.Cards.Where(card => card.ProfileId.Equals(profileId)).ToListAsync();
    
    
    public async Task<List<Card>> GetAllCardsByPhoneNumberAsync(string phoneNumber) =>
        await _appDbContext.Cards.Where(card => card.ProfileId.Equals(phoneNumber)).Include(x => x.Profile).ToListAsync();
    
    
    public async Task<Card> GetCardByIdAsync(int id) =>
        await _appDbContext.Cards.FindAsync(new object[] {id});
    
    public async Task<Card> GetCardByCardNumberAsync(string cardNumber) =>
        await _appDbContext.Cards.Where(card => card.CardNumber == cardNumber).FirstOrDefaultAsync();
    
    public async Task SaveAsync() => 
        await _appDbContext.SaveChangesAsync();

    
    #region Helpers

    public string GenerateCvvCode()
    {
        var rnd = new Random();
        return rnd.Next(100, 999).ToString();
    }

    public string GenerateCardNumber()
    {
        var rnd = new Random();
        int[] checkArray = new int[15];
            
        var cardNum = new int[16];
 
        for (int d = 14; d >= 0; d--)
        {
            cardNum[d] = rnd.Next(0, 9);
            checkArray[d] = ( cardNum[d] * (((d+1)%2)+1)) % 9;
        }
 
        cardNum[15] = ( checkArray.Sum() * 9 ) % 10;
 
        var sb = new StringBuilder(); 
 
        for (int d = 0; d < 16; d++)
        {
            sb.Append(cardNum[d]);
        } 
        return sb.ToString();
    }

    #endregion

}