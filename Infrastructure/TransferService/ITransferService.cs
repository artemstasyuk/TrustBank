namespace BankApplication.Infrastructure.TransferService;

public interface ITransferService
{
    Task<Operation> TransferByCardNumber(int cardId, string cardNumber, decimal amount, CardOperationType type);

    Task<Operation> ReplenishByCardNumber(int cardTo, string cardNumber, decimal amount, string cvv, string validity,
        CardOperationType type);
}