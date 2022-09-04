using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers
{       
    public class CardsController : Controller
    {
        private readonly ICardSampleRepository _cardSampleRepository;
        public CardsController(ICardSampleRepository cardSampleRepository)
        {
            _cardSampleRepository = cardSampleRepository;
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
        public IActionResult Checkout(Card card)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Index");
            return View(card);
        }
    }
}
