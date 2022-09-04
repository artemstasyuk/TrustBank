using BankApplication.Extensions;
using BankApplication.Infrastructure.AuthService;

namespace BankApplication.Controllers.AccountControllers;

public class AuthController : Controller
{
    private readonly IProfileRepository _profileRepository;
    private readonly IUserRepository _userRepository;
    private readonly EmailTokenDto _emailToken;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public AuthController(EmailTokenDto emailToken,
        IProfileRepository profileRepository, 
        IUserRepository userRepository,
        ITokenService tokenService, IEmailService emailService)
    {
        _emailToken = emailToken;
        _emailService = emailService;
        _tokenService = tokenService;
        _profileRepository = profileRepository;
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public ActionResult Login() => View();
    
    
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        
        var user = await _userRepository.GetUserByEmailAsync(viewModel.Email);
        if (user is null) return BadRequest(new {messege = "Not exist"});
        
        if (_userRepository.CheckPassword(viewModel.Password, user)) return BadRequest(new {messege = "Password isn't correct"});

        Authorize(_tokenService.CreateToken(user));

        return RedirectToAction("Index", "Home");
    }

    
    [HttpGet]
    public ActionResult Registration() => View();
    
    
    [HttpPost]
    public async Task<ActionResult> Registration(RegistrationViewModel viewModel)
    {
        if (!ModelState.IsValid) return View();
        if (await _userRepository.UserExist(viewModel.Email)) return BadRequest(new {messege = "Invalid Credentials"});

        var user = new User()
        {
            Email = viewModel.Email,
            Password = viewModel.Password,
            Name = viewModel.AccountName,
            Surname = viewModel.AccountSurname,
        };
        await _userRepository.CreateUser(user);

        await _profileRepository.CreateProfile(user);

        var emailTokenDto = _emailService.SendEmailCode(viewModel.Email); 
        _emailToken.SetEmailToken(emailTokenDto);
        
        return RedirectToAction("VerifyEmailToken");
    }
    
    
    [HttpGet]
    public ActionResult VerifyEmailToken() => View();
    
    
    [HttpPost]
    public async Task<ActionResult> VerifyEmailToken(EmailViewModel emailDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(_emailToken.Email);
        if (emailDto.EmailCode != _emailToken.EmailCode) return BadRequest(new {messege = "Code is not right"});

        var profile = await _profileRepository.GetProfileByUserIdAsync(user.Id);
        await _profileRepository.VerifyEmail(profile);
        Authorize(_tokenService.CreateToken(user));
            
        return RedirectToAction("Index", "Home");
    }
    
    
    public ActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return RedirectToAction("Index", "Home");
    }

    public void Authorize(string token)
    {
        Response.Cookies.Append("jwt", token, new CookieOptions()
        {
            HttpOnly = true
        });
    }
}