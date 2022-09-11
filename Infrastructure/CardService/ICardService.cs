namespace BankApplication.Infrastructure.CardService;

public interface ICardService
{
    Task Checkout(int cardSampleId, int userId, CheckoutViewModel viewModel);
}