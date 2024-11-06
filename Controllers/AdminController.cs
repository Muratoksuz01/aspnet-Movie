using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using movieApp_Web.Data;
using movieApp_Web.Entity;
using movieApp_Web.Models;
/*


 film kısmında 1 haricindekiler sildiginde sorun yok 
*/
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


    public IActionResult movieCreate()
    {
        ViewBag.Genres = _context.Genres.ToList();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> movieCreate(Movie m, int[] genreIds , IFormFile file)
    {
        if (ModelState.IsValid)
        {
            m.Genres = new List<Genre>();
            foreach (var item in genreIds)
            {
                m.Genres.Add(_context.Genres.FirstOrDefault(i => i.GenreId == item));

            }
            if(file !=null){
                var extension=Path.GetExtension(file.FileName);
                var FileName=string.Format($"{Guid.NewGuid()}{extension}");
                var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img",FileName);
                m.Imageurl =FileName;
                using(var stream =new FileStream(path,FileMode.Create)){
                    await file.CopyToAsync(stream);
                }
            }



            _context.Movies.Add(m);
            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }
        ViewBag.Genres = _context.Genres.ToList();
        return View();
    }
    public IActionResult movieUpdate(int? id)
    {
        if (id == null) return NotFound();

        var entity = _context.Movies
        .Select(m => new AdminMoviesModelEdit
        {
            MovieId = m.MovieId,
            Title = m.Title,
            Description = m.Description,
            ImageUrl = m.Imageurl,
            SelectedGenres = m.Genres.ToList()
        })
        .FirstOrDefault(m => m.MovieId == id);
        ViewBag.Genres = _context.Genres.ToList();
        if (entity == null) return NotFound();


        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> movieUpdate(AdminMoviesModelEdit model, int[] genreids, IFormFile file)
    {
        var entity = _context.Movies.Include("Genres").FirstOrDefault(m => m.MovieId == model.MovieId);
        if (entity == null) return NotFound();
        entity.Title = model.Title;
        entity.Description = model.Description;

        if(file !=null){
            var extension=Path.GetExtension(file.FileName);
            var FileName=string.Format($"{Guid.NewGuid()}{extension}");
            var path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img",FileName);
            entity.Imageurl =FileName;
            using(var stream =new FileStream(path,FileMode.Create)){
                await file.CopyToAsync(stream);
            }
        }
        entity.Genres = genreids.Select(id => _context.Genres.FirstOrDefault(i => i.GenreId == id)).ToList();
        _context.SaveChanges();
        return RedirectToAction("MovieList");
    }
  
    public IActionResult GenreList()
    {
        return View(new AdminGenresViewModel
        {
            Genres = _context.Genres.Select(g => new AdminGenreViewModel
            {
                GenreId = g.GenreId,
                Name = g.Name,
                Count = g.Movies.Count
            }).ToList()
        });
    }
    public IActionResult UpdateGenre(int? id)
   
   
   
   
    {
        System.Console.WriteLine(" buraad get foksiyonu calişti");
        if (id == null) return NotFound();
        var entity = _context.Genres
        .Select(g => new AdminGenreEditViewModel
        {
            GenreId = g.GenreId,
            Name = g.Name,
            Movies = g.Movies.Select(m => new AdminMoviesViewModels
            {
                MovieId = m.MovieId,
                Title = m.Title,
                ImageUrl = m.Imageurl
            }).ToList()
        })
        .FirstOrDefault(i => i.GenreId == id);
        if (entity == null) return NotFound();
        return View(entity);

    }
   






   
    [HttpPost]
    public IActionResult UpdateGenre(AdminGenreEditViewModel model, int[]? movieIds, int? genreId)
    {//                         genreId surekli 0 olarak geliyor  aslında model null olarka geliyor 
     

        Console.WriteLine("      burada post calişti ");
        var entity = _context.Genres.Include(m => m.Movies).FirstOrDefault(m => m.GenreId ==genreId);
      //  if (entity == null) return NotFound();
        entity.Name = model.Name;
        foreach (var id in movieIds)
        {
            entity.Movies.Remove(entity.Movies.FirstOrDefault(m => m.MovieId == id));
        }
        _context.SaveChanges();
        return RedirectToAction("GenreList");
    }

    [HttpPost]
    public IActionResult DeleteGenre(int? GenreId)
    {
       var entity=_context.Genres.Find(GenreId);
       if(entity!= null){
        _context.Genres.Remove(entity);
        _context.SaveChanges();
       }

        return RedirectToAction("GenreList");
    }

  [HttpPost]
    public IActionResult DeleteMovie(int? MovieId)
    {
       var entity=_context.Movies.Find(MovieId);
       if(entity!= null){
        _context.Movies.Remove(entity);
        _context.SaveChanges();
       }

        return RedirectToAction("MovieList");
    }
   
   

}