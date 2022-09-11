using BankApplication.Infrastructure.AuthService.EmailService;

namespace BankApplication.Infrastructure.CardService;

public class CardService : ICardService
{
    private readonly IProfileRepository _profileRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IEmailService _emailService;
    private readonly ICardSampleRepository _cardSampleRepository;

    public CardService( IProfileRepository profileRepository, ICardRepository cardRepository, 
        IEmailService emailService, ICardSampleRepository cardSampleRepository)
    {
        _cardSampleRepository = cardSampleRepository;
        _cardRepository = cardRepository;
        _profileRepository = profileRepository;
        _emailService = emailService;
    }
    
    public async Task Checkout(int cardSampleId, int userId, CheckoutViewModel viewModel)
    {
        var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
        
        var cardSample = await _cardSampleRepository.GetCardByIdAsync(cardSampleId);
        await _cardRepository.CreateCardAsync(new Card(){ CardName = viewModel.Name, CardSurname = viewModel.Surname, 
            Type = cardSample.Type}, profile.Id);
        
        _emailService.SendEmailCode(viewModel.Email, $"Your card was send to {viewModel.Adress}",
            "Card checkout");
    }
    
}