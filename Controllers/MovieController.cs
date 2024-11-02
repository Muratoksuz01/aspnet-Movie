using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            movies = movies.Where(m => m.GenreId == id);
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

    public IActionResult Create()
    {
        ViewBag.Genres = _context.Genres.ToList(); //benim yontem burada farklı bir yotem denedim bu yuzden create.cshtml de farklılık var 
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

            _context.Movies.Add(m);
            _context.SaveChanges(); 
            // Console.WriteLine("crete if ici");
            // MovieRapository.Add(m);
            Console.Write("eklendi");
            TempData["Message"] = $"item CREATED eklenen film :{m.Title}";
            return RedirectToAction("List");
        }
        ViewBag.Genres = _context.Genres.ToList();
        return View();

    }

    public IActionResult Edit(int id)
    {
        //  ViewBag.Genres=GenreRepository.Genres; //benim yontem
        ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreId", "Name"); //hocanın yontemi
        var m = _context.Movies.Find(id);
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
            _context.Movies.Update(n);
                        _context.SaveChanges(); 

          //  MovieRapository.Edit(n);
            TempData["Message"] = "item guncellendi";
            return RedirectToAction("Details", new { @id = n.MovieId });


        }
        ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreId", "Name"); //hocanın yontemi
        return View(n);
    }


    public IActionResult Delete(int id)
    {
        _context.Movies.Remove(_context.Movies.Find(id));
                    _context.SaveChanges(); 

        Console.WriteLine("dele foksiyonu");
        TempData["Message"] = "item silindi";
        return RedirectToAction("List");


    }


   
}
