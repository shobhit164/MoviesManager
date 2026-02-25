using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MoviesManager.Models;

namespace MoviesManager.Controllers;

// This controller basically handles the main pages of the website
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

// It will show the error page if something goes wrong
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
