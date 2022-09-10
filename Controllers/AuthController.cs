using BankApplication.Infrastructure.AuthService.UserControlService;

namespace BankApplication.Controllers;

public class AuthController : Controller
{
    private readonly IUserControlService _userControlService;

    public AuthController(IUserControlService userControlService)
    {
        _userControlService = userControlService;
       
    }

    [HttpGet]
    public ActionResult Login() => View();
    
    
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        var authDto = await _userControlService.Login(viewModel);
        if (authDto.Status != true)
        {
            ModelState.AddModelError("", authDto.Error);
            return View();
        } 

        Authorize(authDto.Token);
        return RedirectToAction("Index", "Home");
    }

    
    [HttpGet]
    public ActionResult Registration() => View();
    
    
    [HttpPost]
    public async Task<ActionResult> Registration(RegistrationViewModel viewModel)
    {
        if (!ModelState.IsValid) return View();
        var authDto = await _userControlService.Registration(viewModel);
        if (authDto.Status != true)
        {
            ModelState.AddModelError("", authDto.Error);
            return View();
        } 

        return RedirectToAction("VerifyEmailToken");
    }
    
    
    [HttpGet]
    public ActionResult VerifyEmailToken() => View();
    
    
    [HttpPost]
    public async Task<ActionResult> VerifyEmailToken(EmailViewModel emailDto)
    {
        var authDto = await _userControlService.VerifyEmailToken(emailDto);
        if (authDto.Status != true)
        {
            ModelState.AddModelError("", authDto.Error);
            return View();
        } 
        
        Authorize(authDto.Token);
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