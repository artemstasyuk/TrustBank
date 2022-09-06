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
    
    public async Task<Operation> TransferByCardNumber(int cardId, string cardNumber, decimal amount, CardOperationType type)
    {
        bool isCompleted = false;
        switch (type)
        {
            case CardOperationType.Replenish :
            {
                Card cardFromDb = await _cardRepository.GetCardByCardNumberAsync(cardNumber);
                Card cardToDb = await _cardRepository.GetCardByIdAsync(cardId);
                isCompleted = await CheckBalance(cardFromDb, cardToDb, amount);
                break;
            }
            case CardOperationType.Transfer:
            {
                Card cardFromDb = await _cardRepository.GetCardByIdAsync(cardId);
                Card cardToDb = await _cardRepository.GetCardByCardNumberAsync(cardNumber);
                isCompleted = await CheckBalance(cardFromDb, cardToDb, amount);
                break;
            }
        }

        Operation operation = new Operation()
        {
            RecipientСardNumber = cardNumber,
            Amount = amount, CardId = cardId,
            IsCompleted = isCompleted
        };
        
        await _operationRepository.CreateOperation(operation);
        return operation;
    }

    public async Task<bool> CheckBalance(Card cardFromDb, Card cardToDb, decimal amount)
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