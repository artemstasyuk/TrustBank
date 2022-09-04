namespace BankApplication.Data.Interfaces;

public interface IProfileRepository
{
    Task<Profile> GetProfileByIdAsync(int id);
    Task<Profile> GetProfileByPhoneNumber(string phoneNumber);
    Task<Profile> GetProfileByEmail(string email);
    Task VerifyEmail(Profile profile);
    Task CreateProfile(User user);
    Task ReturnProfileAsync(int profileId);
    Task DeleteProfileAsync(int profileId);
    Task<Profile> GetProfileByUserIdAsync(int userid);
}