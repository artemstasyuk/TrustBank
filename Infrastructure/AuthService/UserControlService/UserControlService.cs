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

    public async Task<AuthDto> Login(LoginViewModel viewModel)
    {
        AuthDto dto = new ();
        if (!await _userRepository.CheckUserCredentials(viewModel.Email, viewModel.Password))
        {
            dto.Error = "Incorrect login or password";
            dto.Status = false;
            return dto;
        }

        dto.Token = _tokenService.CreateToken(await _userRepository.GetUserByEmailAsync(viewModel.Email));
        return dto;
    }

    public async Task<AuthDto> Registration(RegistrationViewModel viewModel)
    {
        AuthDto dto = new();
        if (!await _userRepository.UserExist(viewModel.Email))
        {
            dto.Error = "User exist";
            dto.Status = false;
            return dto;
        }

        var user = new User()
        {
            Email = viewModel.Email,
            Password = viewModel.Password,
            Name = viewModel.AccountName,
            Surname = viewModel.AccountSurname,
        };
        
        await _userRepository.CreateUser(user);

        await _profileRepository.CreateProfile(user);

        var emailTokenDto = _emailService.SendEmailCode(viewModel.Email, GenerateEmailCode(), "Verify your email"); 
        
        _emailToken.SetEmailToken(emailTokenDto);

        return dto;
    }
    public async Task<AuthDto> VerifyEmailToken(EmailViewModel emailDto)
    {
        AuthDto dto = new();
        var user = await _userRepository.GetUserByEmailAsync(_emailToken.Email);
        if (emailDto.EmailCode != _emailToken.EmailCode)
        {
            dto.Error = "Code isn't right";
            dto.Status = false;
            return dto;
        }
        
        await _profileRepository.VerifyEmail(user);
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