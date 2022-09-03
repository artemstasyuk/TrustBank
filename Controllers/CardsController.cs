using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers
{
    public class CardsController : Controller
    {        
        public IActionResult Index() => View();
        
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
