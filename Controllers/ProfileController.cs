using System.Security.Claims;
using BankApplication.Infrastructure.ImageService;
using BankApplication.Infrastructure.ProfileService;
using Microsoft.AspNetCore.Authorization;

namespace BankApplication.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }
    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        return View(await _profileService.GetProfile(userId));
    }
 
    [HttpPost]
    public async Task<IActionResult> EditProfile(ProfileViewModel viewModel)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        await _profileService.EditProfile(viewModel, userId);
        return RedirectToAction("Index");
    }
}