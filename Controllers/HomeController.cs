using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Models;

namespace BankApplication.Controllers;

public class HomeController : Controller
{    
    public HomeController()
    {
       
    }

    public IActionResult Index()
    {
       return View();
    }
}