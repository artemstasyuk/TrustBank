namespace BankApplication.Infrastructure.TransferService;

public interface ITransferService
{
    Task<Operation> TransferByCardNumber(int cardId, string cardNumber, decimal amount, CardOperationType type);
}