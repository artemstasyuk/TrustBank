using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers
{
    public class CardsController : Controller
    {
        [Authorize]
        public IActionResult Index() => View();
        
        [HttpGet]
        public IActionResult Checkout() =>  View();

        /*[HttpPost]
        public IActionResult Checkout(Card card)
        {
            return View(card);
        }*/
    }
}
