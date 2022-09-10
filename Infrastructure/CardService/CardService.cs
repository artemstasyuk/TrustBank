using BankApplication.Infrastructure.AuthService.EmailService;

namespace BankApplication.Infrastructure.CardService;

public class CardService : ICardService
{
    private readonly IProfileRepository _profileRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IEmailService _emailService;

    public CardService( IProfileRepository profileRepository, ICardRepository cardRepository, IEmailService emailService)
    {
        _cardRepository = cardRepository;
        _profileRepository = profileRepository;
        _emailService = emailService;
    }
    
    public async Task Checkout(int userId, CheckoutViewModel viewModel)
    {
        var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
        await _cardRepository.CreateCardAsync(new Card(){ CardName = viewModel.Name, CardSurname = viewModel.Surname}, profile.Id);
        _emailService.SendEmailCode(viewModel.Email, $"Your card was send to {viewModel.Adress}",
            "Card checkout");
    }
    
}