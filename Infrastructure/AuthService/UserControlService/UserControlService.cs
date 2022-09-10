using BankApplication.Infrastructure.AuthService.EmailService;
using BankApplication.Infrastructure.AuthService.JwtTokenService;

namespace BankApplication.Infrastructure.AuthService.UserControlService;

public class UserControlService : IUserControlService 
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IProfileRepository _profileRepository;
    private readonly IEmailService _emailService;
    private readonly EmailTokenDto _emailToken;
    public UserControlService(IUserRepository userRepository, ITokenService tokenService, 
        IProfileRepository profileRepository, IEmailService emailService,
        EmailTokenDto emailToken)
    {
        _emailToken = emailToken;
        _emailService = emailService;
        _profileRepository = profileRepository;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Login action
    /// </summary>
    /// <param name="viewModel">contains user's params</param>
    /// <returns>Auth dto with Error message or Jwt token</returns>
    public async Task<AuthErrorDto> Login(LoginViewModel viewModel)
    {
        AuthErrorDto dto = new ();
        if (await _userRepository.CheckUserCredentials(viewModel.Email, viewModel.Password))
        {
            dto.Status = true;
            dto.Token = _tokenService.CreateToken(await _userRepository.GetUserByEmailAsync(viewModel.Email));
            return dto;
        }
        dto.Error = "Incorrect login or password";
        return dto; 

       
    }

    /// <summary>
    /// Registration action
    /// </summary>
    /// <param name="viewModel">contain user's params</param>
    /// <returns>Auth dto with only Error message </returns>
    public async Task<AuthErrorDto> Registration(RegistrationViewModel viewModel)
    {
        AuthErrorDto dto = new();
        if (await _userRepository.UserExist(viewModel.Email))
        {
            var user = new User()
            {
                Email = viewModel.Email,
                Password = viewModel.Password,
                Name = viewModel.AccountName,
                Surname = viewModel.AccountSurname,
            };
        
            await _userRepository.CreateUser(user);

            await _profileRepository.CreateProfile(user);

            var emailTokenDto = _emailService.SendEmailCode(viewModel.Email, $"This is your code {GenerateEmailCode()}", "Verify your email"); 
        
            _emailToken.SetEmailToken(emailTokenDto);

            dto.Status = true;
            return dto;
        }
        dto.Error = "User exist";
        return dto;
    }
    
    /// <summary>
    /// Email verify action
    /// </summary>
    /// <param name="emailDto">contain email code</param>
    /// <returns>Auth dto with Error message or Jwt token</returns>
    public async Task<AuthErrorDto> VerifyEmailToken(EmailViewModel emailDto)
    {
        AuthErrorDto dto = new();
        var user = await _userRepository.GetUserByEmailAsync(_emailToken.Email);
        if (emailDto.EmailCode != _emailToken.EmailCode)
        {
            dto.Error = "Code isn't right";
            return dto;
        }
        
        await _profileRepository.VerifyEmail(user);
        dto.Status = true;
        dto.Token = _tokenService.CreateToken(user);
        return dto;
    }
    
    private string GenerateEmailCode()
    {
        int _min = 000000;
        int _max = 999999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max).ToString();
    }
}