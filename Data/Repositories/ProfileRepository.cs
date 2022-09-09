using System.Security.Cryptography;
using System.Text;

namespace BankApplication.Data.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly AppDbContext _dbContext;
    public ProfileRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Profile> GetProfileByIdAsync(int id) =>
        await _dbContext.UsersProfiles.FindAsync(new object[] {id});
    
    public async Task<Profile> GetProfileByUserIdAsync(int userid) =>
        await _dbContext.UsersProfiles.Include(user => user.AvatarModel).FirstOrDefaultAsync(user => user.UserId == userid);

    public async Task<Profile> GetProfileByPhoneNumber(string phoneNumber) =>
        await _dbContext.UsersProfiles.FindAsync(new object[] {phoneNumber});

    public async Task<Profile> GetProfileByEmail(string email) => 
         await _dbContext.UsersProfiles.Where(p => p.Email == email).FirstOrDefaultAsync();
    
    public async Task VerifyEmail(Profile profile)
    {
        profile.IsVerified = true;
        await SaveAsync();
    }

    public async Task CreateProfile(User user)
    {
        await _dbContext.UsersProfiles.AddAsync(new Profile()
        {
            UserId = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsVerified = false,
            Status = ProfileStatus.Active
        });
        await SaveAsync();
    }
    
    public async Task ReturnProfileAsync(int profileId)
    {
        var profile = await _dbContext.UsersProfiles.FindAsync(new object[] {profileId});
        profile.Status = ProfileStatus.Returned;
        await SaveAsync();
    }

    public async Task DeleteProfileAsync(int profileId)
    {
        var profile = await _dbContext.UsersProfiles.FindAsync(new object[] {profileId});
        profile.Status = ProfileStatus.Deleted;
        await SaveAsync();
    }
    public async Task SaveAsync() => 
        await _dbContext.SaveChangesAsync();

    public async Task EditProfileAsync(int profileId, ProfileViewModel profileModel, AvatarModel avatar)
    {
        var profile = await _dbContext.UsersProfiles.FindAsync(new object[] { profileId });
        
        profile.Name = profileModel.Profile.Name;
        profile.Surname = profileModel.Profile.Surname;
        if (avatar.Id != 0)
            profile.AvatarModel = avatar; 
        await SaveAsync();
    }

    #region Helpers

    public string GetHash(string data)
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(data);
        byte[] hash = md5.ComputeHash(inputBytes);
        // step 2, convert byte array to hex string
        var sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }   
    #endregion
}