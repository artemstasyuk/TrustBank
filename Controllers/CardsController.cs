using System.Security.Claims;
using BankApplication.Infrastructure.CardService;


namespace BankApplication.Controllers
{       
    public class CardsController : Controller
    {

        private readonly ICardSampleRepository _cardSampleRepository;
        private readonly ICardService _cardService;
        public CardsController(ICardSampleRepository cardSampleRepository, ICardService cardService)
        {
            _cardService = cardService;
            _cardSampleRepository = cardSampleRepository;
        }
        public async Task<IActionResult> Index(string type)
        {
            List<CardSample> samples;
            if(type == null)
            {
                samples = await _cardSampleRepository.GetAll();
                return View(samples);
            }
            samples = await _cardSampleRepository.GetCardsByType(type);
            return View(samples);
            
        }
            
        
        [HttpGet]
        public IActionResult Checkout() =>  View();

        [HttpPost]
        public async Task<IActionResult> Checkout(int id,CheckoutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                await _cardService.Checkout(id, userId, viewModel);
                
                
                return RedirectToAction("Index", "Profile");
            }
            return View(viewModel);
        }
    }
}
