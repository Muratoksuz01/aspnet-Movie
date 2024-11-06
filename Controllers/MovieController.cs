using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using movieApp_Web.Data;
using movieApp_Web.Entity;
using movieApp_Web.Models;

namespace movieApp_Web.Controllers;

public class MovieController : Controller
{
    private  readonly MovieContext _context;

    public MovieController( MovieContext context)
    {
        _context=context;
    }
    public IActionResult Index()
    {
        Console.WriteLine("index movies");
        return View();
    }

    //http://localhost:5076/Movie/list
    //http://localhost:5076/Movie/list/1
    public IActionResult List(int? id, string? q) // id gelsede gelmesede burası calişacak 
    {
        Console.WriteLine("list foksiyonu");

        // var controller= RouteData.Values["controller"];
        // var action= RouteData.Values["action"];
        // var genreid= RouteData.Values["id"];
        var movies = _context.Movies.AsQueryable();// buradan gelen biligiyi filteyebilir sekilde getiriyor 
        if (id != null)
        {
            movies = movies
            .Include(m=>m.Genres)
            .Where(m => m.Genres.Any(g=>g.GenreId==id));
        }
        if (!string.IsNullOrEmpty(q))
        {
            movies = movies.Where(
            m => m.Title.ToLower().Contains(q.ToLower()) ||
            m.Description.ToLower().Contains(q.ToLower()));
        }
        var model = new MovieViewModel()
        {
            Movies = movies.ToList()
        };
        return View("Movies", model);
    }

    //http://localhost:5076/Movie/Details/1
    public IActionResult Details(int id) => View(_context.Movies.Find(id));

   

   
}
