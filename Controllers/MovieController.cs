using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using movieApp_Web.Data;
using movieApp_Web.Models;

namespace movieApp_Web.Controllers;

public class MovieController : Controller
{
    private readonly ILogger<MovieController> _logger;

    public MovieController(ILogger<MovieController> logger)
    {
        _logger = logger;
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
        var movies = MovieRapository.Movies;
        if (id != null)
        {
            movies = movies.Where(m => m.GenreId == id).ToList();
        }
        if (!string.IsNullOrEmpty(q))
        {
            movies = movies.Where(
            m => m.Title.ToLower().Contains(q.ToLower()) ||
            m.Description.ToLower().Contains(q.ToLower())).ToList();
        }
        var model = new MovieViewModel()
        {
            Movies = movies
        };
        return View("Movies", model);
    }

    //http://localhost:5076/Movie/Details/1
    public IActionResult Details(int id) => View(MovieRapository.GetById(id));

    public IActionResult Create()
    {
        ViewBag.Genres = GenreRepository.Genres; //benim yontem burada farklı bir yotem denedim bu yuzden create.cshtml de farklılık var 
        return View();
    }

    [HttpPost]
    public IActionResult Create(Movie m
      // tum bunalr yerine 'Movie m' dersen yine aynı işlemmi alırsın cınki 
      //formdan gelen name iismleriile classtaki isimleri aynı 
      //  string Title, string Description, string Director, string ImageUrl, int GenreId
      )
    {
        Console.WriteLine("creare fok");
        // var m= new Movie()
        // {
        //     Title=Title,
        //     Description=Description,
        //     Director=Director,
        //     Imageurl=ImageUrl,
        //     GenreId=GenreId

        // };

        if (ModelState.IsValid)
        {
            Console.WriteLine("crete if ici");
            MovieRapository.Add(m);
            Console.Write("eklendi");
            TempData["Message"] = "item CREATED";
            return RedirectToAction("List");
        }
        ViewBag.Genres = GenreRepository.Genres;
        return View();

    }

    public IActionResult Edit(int id)
    {
        //  ViewBag.Genres=GenreRepository.Genres; //benim yontem
        ViewBag.Genres = new SelectList(GenreRepository.Genres, "GenreId", "Name"); //hocanın yontemi
        var m = MovieRapository.GetById(id);
        return View(m);
    }
    [HttpPost]
    public IActionResult Edit(int id, Movie n)
    {
        // Console.WriteLine(n);
        /* benim yontem
        var m=MovieRapository.GetById(id);
        m.Title=n.Titl   e;
        Console.Write("guncellerindi :)");
        return RedirectToAction("List");
        */

        if (ModelState.IsValid)
        {
            MovieRapository.Edit(n);
            TempData["Message"] = "item guncellendi";
            return RedirectToAction("Details", new { @id = n.MovieId });


        }
        ViewBag.Genres = new SelectList(GenreRepository.Genres, "GenreId", "Name"); //hocanın yontemi
        return View(n);
    }


    public IActionResult Delete(int id)
    {
        MovieRapository.Del(id);
        Console.WriteLine("dele foksiyonu");
        TempData["Message"] = "item silindi";
        return RedirectToAction("List");


    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
