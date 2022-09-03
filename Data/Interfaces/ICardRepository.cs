namespace BankApplication.Data.Interfaces;

public interface ICardRepository
{
    Task CreateCardAsync(Card card);
    Task DeleteCardAsync(int cardId);
    Task ReturnCardAsync(int cardId);
    Task UpdateCardInitialsAsync(Card card);
    Task<List<Card>> GetAllCardsByAccIdAsync(string accId);
    Task<List<Card>> GetAllCardsByPhoneNumberAsync(string phoneNumber);
    Task<Card> GetCardByIdAsync(int id);
    Task<Card> GetCardByCardNumberAsync(string cardNumber);
    Task SaveAsync();
}