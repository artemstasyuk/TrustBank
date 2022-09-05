using BankApplication.Infrastructure.TransferService;
using BankApplication.Views.Transfer;

namespace BBankApplication.Infrastructure.TransferService;

public class TransferService : ITransferService
{
    private readonly ICardRepository _cardRepository;

    public TransferService(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    
    public async Task<Operation> TransferByCardNumber(int cardId, string cardNumber, double amount)
    {
        Card cardFromDb = await _cardRepository.GetCardByIdAsync(cardId);
        Card cardToDb = await _cardRepository.GetCardByCardNumberAsync(cardNumber);
        //if (cardToDb is null)

        //if (cardFromDb.Balance < amount) 

        cardFromDb.Balance -= amount;
        cardToDb.Balance += amount;
        await _cardRepository.SaveAsync();
        return new Operation() {RecipientСardNumber = cardToDb.CardNumber, Amount = amount, IsCompleted = true};
    }
}