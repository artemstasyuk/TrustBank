using BankApplication.Models.Enums;

namespace BankApplication.Data.Repositories
{
    public class CardSampleRepository : ICardSampleRepository
    {
        private readonly AppDbContext _db;
        
        public CardSampleRepository(AppDbContext db)
        {
            _db = db;
        }
        
        public async Task<List<CardSample>> GetAll() =>
            await _db.CardSamples.Include(s => s.Features).ToListAsync();

        public async Task<CardSample> GetCardByIdAsync(int id) =>
            await _db.CardSamples.FindAsync(new object[] {id});
       
        public async Task<List<CardSample>> GetCardsByType(string type)
        {
            CardType cardType = (CardType)Enum.Parse(typeof(CardType), type);
            return await _db.CardSamples.Include(s => s.Features).Where(el => el.Type == cardType).ToListAsync();           
        }
                   
        public async Task CreateSample(CardSample cardSample)
        {
            await _db.CardSamples.AddAsync(cardSample);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteSample(int cardId)
        {
            var sample = await _db.CardSamples.FindAsync(new object[] { cardId });
            _db.CardSamples.Remove(sample);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateSample(int cardId)
        {
            var sample = await _db.CardSamples.FindAsync(new object[] { cardId });
            _db.CardSamples.Update(sample);
            await _db.SaveChangesAsync();
        }
    }
}
