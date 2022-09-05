using System.Security.Claims;
using BankApplication.Infrastructure.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers
{       
    public class CardsController : Controller
    {
        private readonly ICardSampleRepository _cardSampleRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IEmailService _emailService;
        public CardsController(ICardSampleRepository cardSampleRepository, IProfileRepository profileRepository, 
            ICardRepository cardRepository, IEmailService emailService)
        {
            _cardRepository = cardRepository;
            _profileRepository = profileRepository;
            _cardSampleRepository = cardSampleRepository;
            _emailService = emailService;
        }
        
        public async Task<IActionResult> Index(string type)
        {
            var samples = new List<CardSample>();
            if(type == null)
            {
                samples = await _cardSampleRepository.GetAll();
                return View(samples);
            } 
               
            else
            {
                samples = await _cardSampleRepository.GetCardsByType(type);
                return View(samples);
            }
        }
            
        
        [HttpGet]
        public IActionResult Checkout() =>  View();

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
                await _cardRepository.CreateCardAsync(new Card(){ CardName = viewModel.Name, CardSurname = viewModel.Surname}, profile.Id);
                _emailService.SendEmailCode(viewModel.Email, $"Your card was send to {viewModel.Adress}",
                    "Card checkout");
                
                return RedirectToAction("Index", "Profile");
            }
            return View(viewModel);
        }
    }
}
