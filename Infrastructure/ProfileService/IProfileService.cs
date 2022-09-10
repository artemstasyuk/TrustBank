namespace BankApplication.Infrastructure.ProfileService;

public interface IProfileService
{
    Task<ProfileViewModel> GetProfile(int userId);
    Task EditProfile(ProfileViewModel viewModel, int userId);
}