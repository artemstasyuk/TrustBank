namespace BankApplication.Data.Interfaces
{
    public interface ICardSampleRepository
    {
        Task<List<CardSample>> GetAll();
        Task<List<CardSample>> GetCardsByType(string type);
        Task CreateSample(CardSample cardSample);
        Task DeleteSample(int cardId);
        Task UpdateSample(int cardId);
    }
}
