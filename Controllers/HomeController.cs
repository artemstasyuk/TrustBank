using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Models;

namespace BankApplication.Controllers;

public class HomeController : Controller
{      
    public IActionResult Index()
    {
       return View();
    }
}