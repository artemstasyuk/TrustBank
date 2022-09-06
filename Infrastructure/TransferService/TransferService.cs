using BankApplication.Infrastructure.TransferService;
using BankApplication.Views.Transfer;

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
    
    public async Task<Operation> TransferByCardNumber(int cardId, string cardNumber, double amount)
    {
        Card cardFromDb = await _cardRepository.GetCardByIdAsync(cardId);
        Card cardToDb = await _cardRepository.GetCardByCardNumberAsync(cardNumber);
        //if (cardToDb is null)

        //if (cardFromDb.Balance < amount) 

        cardFromDb.Balance -= amount;
        cardToDb.Balance += amount;
        await _cardRepository.SaveAsync();
        
        Operation operation = new Operation()
        {
            RecipientСardNumber = cardNumber, 
            Amount = amount, CardId = cardId, 
            IsCompleted = true
        };
        
        await _operationRepository.CreateOperation(operation);
        return operation;
    }
}