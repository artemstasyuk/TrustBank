using System.Security.Claims;
using BankApplication.Infrastructure.ImageService;
using Microsoft.AspNetCore.Authorization;

namespace BankApplication.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IProfileRepository _profileRepository;    
    private readonly ICardRepository _cardRepository;
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly IFileUploadService _fileUploadService;

    public ProfileController(IProfileRepository profileRepository, ICardRepository cardRepository, 
        IWebHostEnvironment appEnvironment, IFileUploadService fileUploadService)
    {
        _cardRepository = cardRepository;      
        _profileRepository = profileRepository;
        _appEnvironment = appEnvironment;
        _fileUploadService = fileUploadService;
    }
        
    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
        profile.Cards = await _cardRepository.GetAllCardsByProfileIdAsync(profile.Id);       
        profile.AvatarModel ??= new AvatarModel() {Name = "placeholder", Path = "/img/placeholder.png"};

        var profileViewModel = new ProfileViewModel() { Profile = profile};
        return View(profileViewModel);
    }
 
    [HttpPost]
    public async Task<IActionResult> EditProfile(ProfileViewModel profileModel)
    {
        var avatarFile = await _fileUploadService.LoadFile(profileModel.PhotoProfile, _appEnvironment);
        var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        await _profileRepository.EditProfileAsync(userId, profileModel, avatarFile);
                
        return RedirectToAction("Index");
    }
}