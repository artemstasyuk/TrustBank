namespace BankApplication.Infrastructure.TransferService;

public interface ITransferService
{
    Task<OperationErrorDto> TransferByCardNumber(int cardFromId, string cardNumber, decimal amount, CardOperationType type);

    Task<OperationErrorDto> ReplenishByCardNumber(int cardTo, string cardNumber, decimal amount, string cvv, string validity,
        CardOperationType type);
}