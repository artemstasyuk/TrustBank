using BankApplication.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata;

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
    
    public async Task<OperationErrorDto> TransferByCardNumber(int cardFromId, string cardNumber, decimal amount, CardOperationType type)
    {
        var cardToDb = await GetCard(cardNumber);
        if (cardToDb is not null)
        {
            Card cardFromDb = await _cardRepository.GetCardByIdAsync(cardFromId);
            if (await IsSufficientBalance(cardFromDb, cardToDb, amount))
            {

                Operation operation = new()
                {
                    CardOperationType = type,
                    RecipientСardNumber = cardNumber,
                    CardFromId = cardFromDb.Id,
                    CardToId = cardToDb.Id,
                    Amount = amount,
                    IsCompleted = true
                };

                await _operationRepository.CreateOperation(operation);

                return new OperationErrorDto() {Operation = operation, Status = true};
            }

            return new OperationErrorDto(){Error = "The balance is less than the payment amount"};
        }
        return new OperationErrorDto(){Error = "Card doesn't exist"};
    } 
    
    
    public async Task<OperationErrorDto> ReplenishByCardNumber(int cardTo, string cardNumber, decimal amount, string cvv, string validity, CardOperationType type)
    {
        var cardFromDb = await GetCard(cardNumber);
        if (cardFromDb is not  null)
        {
            if (CheckCardCredentials(cvv, validity, cardFromDb))
            {
                Card cardToDb = await _cardRepository.GetCardByIdAsync(cardTo);
                
                if (await IsSufficientBalance(cardFromDb, cardToDb, amount))
                {
                    Operation operation = new()
                    {
                        CardOperationType = type,
                        RecipientСardNumber = cardNumber,
                        CardFromId = cardFromDb.Id,
                        CardToId = cardToDb.Id,
                        Amount = amount,
                        IsCompleted = true
                    };

                    await _operationRepository.CreateOperation(operation);
                    return new OperationErrorDto() {Operation = operation, Status = true};
                    
                }
                return new OperationErrorDto(){Error = "The balance is less \nthan the payment amount"};
            }
            return new OperationErrorDto(){Error = "Invalid card credentials"};
        }
        return new OperationErrorDto(){Error = "Card doesn't exist"};
    }

    private async Task<Card> GetCard(string cardNumber)
    {
        var card = await _cardRepository.GetCardByCardNumberAsync(cardNumber);
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
