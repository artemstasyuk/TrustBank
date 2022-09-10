using BankApplication.Infrastructure.ImageService;

namespace BankApplication.Infrastructure.ProfileService;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _profileRepository;    
    private readonly ICardRepository _cardRepository;
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly IFileUploadService _fileUploadService;

    public ProfileService(IProfileRepository profileRepository, ICardRepository cardRepository, 
        IWebHostEnvironment appEnvironment, IFileUploadService fileUploadService)
    {
        _cardRepository = cardRepository;      
        _profileRepository = profileRepository;
        _appEnvironment = appEnvironment;
        _fileUploadService = fileUploadService;
    }

    public async Task<ProfileViewModel> GetProfile(int userId)
    {
        var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
        profile.Cards = await _cardRepository.GetAllCardsByProfileIdAsync(profile.Id);       
        profile.AvatarModel ??= new AvatarModel() {Name = "placeholder", Path = "/img/placeholder.png"};

        return new ProfileViewModel() { Profile = profile};
    }

    public async Task EditProfile(ProfileViewModel viewModel, int userId)
    {
        var avatarFile = await _fileUploadService.LoadFile(viewModel.PhotoProfile, _appEnvironment);
        await _profileRepository.EditProfileAsync(userId, viewModel, avatarFile);
    }
}