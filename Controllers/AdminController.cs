using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieApp_Web.Data;
using movieApp_Web.Entity;
using movieApp_Web.Models;

namespace movieApp_Web.Controllers;

public class AdminController : Controller
{
    private readonly MovieContext _context;

    public AdminController(MovieContext context)
    {
        _context = context;
    }
    public IActionResult MovieList()
    {
        return View(new AdminMoviesViewModel
        {
            Movies = _context.Movies
                    .Include(g => g.Genres)
                    .Select(g => new AdminMoviesViewModels
                    {
                        MovieId = g.MovieId,
                        Title = g.Title,
                        ImageUrl = g.Imageurl,
                        Genres = g.Genres.ToList()
                    }).ToList()
        });
    }

    public IActionResult movieUpdate(int? id){
    if(id ==null)return NotFound(); 
    
    var entity=_context.Movies
    .Select(m=>new AdminMoviesModelEdit{
        MovieId=m.MovieId,
        Title=m.Title,
        Description=m.Description,
        ImageUrl=m.Imageurl,
        SelectedGenres=m.Genres.ToList()
    })
    .FirstOrDefault(m =>m.MovieId==id);
    ViewBag.Genres=_context.Genres.ToList();
    if(entity ==null) return NotFound();


    return View(entity);
    }

    [HttpPost]
    public IActionResult movieUpdate(AdminMoviesModelEdit model ,int[] genreids ){
        var entity= _context.Movies.Include("Genres").FirstOrDefault(m=>m.MovieId==model.MovieId);
        if(entity==null)return NotFound();
        entity.Title=model.Title;
        entity.Description=model.Description;
        entity.Imageurl=model.ImageUrl;
        entity.Genres= genreids.Select(id=> _context.Genres.FirstOrDefault(i=>i.GenreId ==id)).ToList();
        _context.SaveChanges();
        return RedirectToAction("MovieList");
    }

    public IActionResult movieDelete(int id)
    {
        var movie = _context.Movies.Where(m => m.MovieId == id).FirstOrDefault();
        _context.Movies.Remove(movie);
        _context.SaveChanges();


        return View("MovieList");
    }

    public IActionResult GenreList(){
        return View(new AdminGenresViewModel{
            Genres=_context.Genres.Select(g=> new AdminGenreViewModel{
                GenreId =g.GenreId,
                Name =g.Name,
                Count =g.Movies.Count
            }).ToList()
        });
    }
  
    public IActionResult UpdateGenre(int? id){
        if(id==null)return NotFound();
        var entity=_context.Genres
        .Select(g=>new AdminGenreEditViewModel{
            GenreId=g.GenreId,
            Name=g.Name,
            Movies=g.Movies.Select(m=> new AdminMoviesViewModels{
                MovieId=m.MovieId,
                Title=m.Title,
                ImageUrl=m.Imageurl
            }).ToList()
        })
        .FirstOrDefault(i=>i.GenreId==id);
        if (entity==null) return NotFound();
        return View(entity);  

    }

}