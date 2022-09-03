namespace BankApplication.Infrastructure;

public class TransferOperation : ITransferOperation
{
    private readonly ICardRepository _cardRepository;

    public TransferOperation(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<string> TransferByPhoneNumber(Card card, string phoneNumber, double amount)
    {
        Card cardFromDb = await _cardRepository.GetCardByIdAsync(card.Id);
        List<Card> cardToDb = await _cardRepository.GetAllCardsByPhoneNumberAsync(phoneNumber);
        if (cardToDb[0] is null) return "Card not found used";

        if (cardFromDb.Balance < amount) return "not enough money";

        cardFromDb.Balance -= amount;
        cardToDb[0].Balance += amount;
        await _cardRepository.SaveAsync();
        return "Success";
    }

    public async Task<string> TransferByCardNumber(Card card, string cardNumber, double amount)
    {
        Card cardFromDb = await _cardRepository.GetCardByIdAsync(card.Id);
        Card cardToDb = await _cardRepository.GetCardByCardNumberAsync(cardNumber);
        if (cardToDb is null) return "Card not found used";

        if (cardFromDb.Balance < amount) return "not enough money";

        cardFromDb.Balance -= amount;
        cardToDb.Balance += amount;
        await _cardRepository.SaveAsync();
        return "Success";
    }
}