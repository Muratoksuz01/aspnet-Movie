using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movieApp_Web.Data;
using movieApp_Web.Models;

namespace movieApp_Web.Controllers;

public class HomeController : Controller
{
    private  readonly MovieContext _context;

    public HomeController( MovieContext context)
    {
        _context=context;
    }

    public IActionResult About()
    {
        
        return View();
    }
    public IActionResult Index()
    {
        var model=new HomePageViewModel(){
            PopulerMovies= _context.Movies.ToList()
        };
    
        return View(model);
    }




}
