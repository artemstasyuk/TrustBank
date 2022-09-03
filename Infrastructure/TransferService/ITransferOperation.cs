namespace BankApplication.Infrastructure;

public interface ITransferOperation
{
    Task<string> TransferByPhoneNumber(Card card, string phoneNumber, double amount);
    Task<string> TransferByCardNumber(Card card, string cardNumber, double amount);
}