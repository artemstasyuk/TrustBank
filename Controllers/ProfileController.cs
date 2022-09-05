using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BankApplication.Controllers;

public class ProfileController : Controller
{
    private readonly IProfileRepository _profileRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    
    public ProfileController(IProfileRepository profileRepository, IUserRepository userRepository, ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
        _userRepository = userRepository;
        _profileRepository = profileRepository;
    }
    
    [Authorize]
    public async Task<ActionResult> Index()
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
        profile.Cards = await _cardRepository.GetAllCardsByProfileIdAsync(profile.Id);
        return View(profile);
    }
}