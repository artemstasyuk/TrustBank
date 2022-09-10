namespace BankApplication.Infrastructure.CardService;

public interface ICardService
{
    Task Checkout(int userId, CheckoutViewModel viewModel);
}