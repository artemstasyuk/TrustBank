using BankApplication.Infrastructure.AuthService;

namespace BankApplication.Controllers.AccountControllers;

public class AuthController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly ISmsService _smsService;
    private readonly EmailTokenDto _emailToken;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public AuthController(EmailTokenDto emailToken,
        IAccountRepository accountRepository, ISmsService smsService,
        ITokenService tokenService, IEmailService emailService)
    {
        _emailToken = emailToken;
        _emailService = emailService;
        _smsService = smsService;
        _tokenService = tokenService;
        _accountRepository = accountRepository;
    }
    
    [HttpGet]
    public ActionResult Login() => View();
    
    
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var account = await _accountRepository.GetAccountByEmail(viewModel.Email);

        if (await _accountRepository.AccountExist(viewModel.Email)) return BadRequest(new {messege="Already Exist"});
        if (_accountRepository.CheckPassword(viewModel.Password, account)) return BadRequest(new {messege = "Password isn't correct"});

        Authorize(_tokenService.CreateToken(account));

        return RedirectToAction("Index", "Home");
    }

    
    [HttpGet]
    public ActionResult Registration() => View();
    
    
    [HttpPost]
    public async Task<ActionResult> Registration(RegistrationViewModel viewModel)
    {
        if (!ModelState.IsValid) return View();
        if (await _accountRepository.AccountExist(viewModel.Email)) return BadRequest(new {messege = "Invalid Credentials"});

        var result = _accountRepository.CreateAccount(new Account()
        {
            PhoneNumber = viewModel.PhoneNumber,
            Email = viewModel.Email,
            Password = viewModel.Password,
            AccountName = viewModel.AccountName,
            AccountSurname = viewModel.AccountSurname,
            IsVerified = false
        });

        var emailTokenDto = _emailService.SendEmailCode(viewModel.Email); 
        _emailToken.SetEmailToken(emailTokenDto);
        
        return RedirectToAction("VerifyEmailToken");
    }
    
    
    [HttpGet]
    public ActionResult VerifyEmailToken() => View();
    
    
    [HttpPost]
    public async Task<ActionResult> VerifyEmailToken(EmailViewModel emailDto)
    {
        var acc = await _accountRepository.GetAccountByEmail(_emailToken.Email);
        if (emailDto.EmailCode != _emailToken.EmailCode) return BadRequest(new {messege = "Code is not right"});
        
        await _accountRepository.VerifyEmail(acc);
        Authorize(_tokenService.CreateToken(acc));
            
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
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