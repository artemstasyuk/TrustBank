namespace BankApplication.Infrastructure.TransferService;

public class TransferService : ITransferService
{
    private readonly ICardRepository _cardRepository;
    private readonly IOperationRepository _operationRepository;

    public TransferService(ICardRepository cardRepository, IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
        _cardRepository = cardRepository;
    }
    
    public async Task<Operation> TransferByCardNumber(int cardFromId, string cardNumber, decimal amount, CardOperationType type)
    {
        var cardToDb = await GetCard(cardNumber);
        Card cardFromDb = await _cardRepository.GetCardByIdAsync(cardFromId);
        
        Operation operation = new()
        {
            CardOperationType = type,
            RecipientСardNumber = cardNumber,
            CardFromId = cardFromDb.Id,
            CardToId = cardToDb.Id,
            Amount = amount, 
            IsCompleted = await IsSufficientBalance(cardFromDb, cardToDb, amount)
        };
        
        await _operationRepository.CreateOperation(operation);
        return operation;
    } 
    public async Task<Operation> ReplenishByCardNumber(int cardTo, string cardNumber, decimal amount, string cvv, string validity, CardOperationType type)
    {
        var cardFromDb = await GetCard(cardNumber);
        if (CheckCardCredentials(cvv, validity, cardFromDb))
        {

            Card cardToDb = await _cardRepository.GetCardByIdAsync(cardTo);

            Operation operation = new()
            {
                CardOperationType = type,
                RecipientСardNumber = cardNumber,
                CardFromId = cardFromDb.Id,
                CardToId = cardToDb.Id,
                Amount = amount,
                IsCompleted = await IsSufficientBalance(cardFromDb, cardToDb, amount)
            };

            await _operationRepository.CreateOperation(operation);
            return operation;
        }

        throw new BadHttpRequestException("Invalid card credentials");
    }

    private async Task<Card> GetCard(string cardNumber)
    {
        var card = await _cardRepository.GetCardByCardNumberAsync(cardNumber);
        if (card is null) throw new BadHttpRequestException("Card doesn't exist");
        return card;
    }

    private bool CheckCardCredentials(string cvv, string validity, Card card) =>
        card.CVV.Equals(cvv) && card.Validity.Equals(validity);
            

    private async Task<bool> IsSufficientBalance(Card cardFromDb, Card cardToDb, decimal amount)
    {
        if(cardFromDb.Balance >= amount)
        {
            cardFromDb.Balance -= amount;
            cardToDb.Balance += amount;
            await _cardRepository.SaveAsync();
            return true;
        }
        return false;
    }

}
