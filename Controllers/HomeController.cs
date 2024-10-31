using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movieApp_Web.Data;
using movieApp_Web.Models;

namespace movieApp_Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult About()
    {
        
        return View();
    }
    public IActionResult Index()
    {
        var model=new HomePageViewModel(){
            PopulerMovies= MovieRapository.Movies
        };
    
        return View(model);
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
