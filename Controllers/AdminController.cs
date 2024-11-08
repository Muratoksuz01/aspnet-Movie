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
        return View(new AdminCreateViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> movieCreate(AdminCreateViewModel m)
    {
        System.Console.WriteLine("Fonksiyon girdi");
        if (m.Title != null && m.Title.Contains("@"))
        {
            ModelState.AddModelError("Title", "title @ iceremez ");
        }

        if (ModelState.IsValid)
        {
            var entity = new Movie()
            {
                Title = m.Title,
                Description = m.Description
            };
            foreach (var item in m.GenreIds)
            {
                entity.Genres.Add(_context.Genres.FirstOrDefault(i => i.GenreId == item));
            }
            if (m.file != null)
            {
                var extension = Path.GetExtension(m.file.FileName);
                var FileName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", FileName);
                entity.Imageurl = FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await m.file.CopyToAsync(stream);
                }
            }
            _context.Movies.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }



        ViewBag.Genres = _context.Genres.ToList();
        return View(m);
    }

    public IActionResult movieUpdate(int? id)
    {
        if (id == null) return NotFound();
        if (ModelState.IsValid)
        {
            var entity = _context.Movies
            .Select(m => new AdminMoviesModelEdit
            {
                MovieId = m.MovieId,
                Title = m.Title,
                Description = m.Description,
                ImageUrl = m.Imageurl,
                GenreIds = m.Genres.Select(i => i.GenreId).ToArray()
            })
            .FirstOrDefault(m => m.MovieId == id);

            ViewBag.Genres = _context.Genres.ToList();

            if (entity == null) return NotFound();

            return View(entity);
        }
        ViewBag.Genres = _context.Genres.ToList();
        return View();


    }

    [HttpPost]
    public async Task<IActionResult> movieUpdate(AdminMoviesModelEdit model, int[] genreids, IFormFile file)
    {
        ModelState.Remove("file");

        if (ModelState.IsValid)
        {
            var entity = _context.Movies.Include("Genres").FirstOrDefault(m => m.MovieId == model.MovieId);
            if (entity == null) return NotFound();
            entity.Title = model.Title;
            entity.Description = model.Description;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                var FileName = string.Format($"{Guid.NewGuid()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", FileName);
                entity.Imageurl = FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }



            entity.Genres = genreids.Select(id => _context.Genres.FirstOrDefault(i => i.GenreId == id)).ToList();
            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }

        var errors = ModelState;
        foreach (var entry in errors)
        {
            System.Console.WriteLine("Key: " + entry.Key);  // Property adı (örneğin: Title, Description)
            foreach (var item in entry.Value.Errors)
            {
                System.Console.WriteLine("Error Message: " + item.ErrorMessage);  // Hata mesajı
            }
        }

        ViewBag.Genres = _context.Genres.ToList();
        return View(model);
    }

    private AdminGenresViewModel GetGenres()
    {
        return new AdminGenresViewModel
        {
            Genres = _context.Genres.Select(g => new AdminGenreViewModel
            {
                GenreId = g.GenreId,
                Name = g.Name,
                Count = g.Movies.Count
            }).ToList()
        };
    }

    public IActionResult CreateGenre(AdminGenresViewModel m)
    {
        ModelState.Remove("Genres");
        if (m.Name != null && m.Name.Length < 3) ModelState.AddModelError("Name", "min 3 karaketer olmalı ");
        if (ModelState.IsValid)
        {
            System.Console.WriteLine("burada ");
            _context.Genres.Add(new Genre { Name = m.Name });
            _context.SaveChanges();
            return RedirectToAction("GenreList");
        }
        var errors = ModelState;
        foreach (var entry in errors)
        {
            System.Console.WriteLine("Key: " + entry.Key);  // Property adı (örneğin: Title, Description)
            foreach (var item in entry.Value.Errors)
            {
                System.Console.WriteLine("Error Message: " + item.ErrorMessage);  // Hata mesajı
            }
        }
        return View("GenreList", GetGenres());
    }
    public IActionResult GenreList()
    {
        return View(GetGenres());
    }


    [HttpPost]
    public IActionResult DeleteGenre(int? GenreId)
    {
        var entity = _context.Genres.Find(GenreId);
        if (entity != null)
        {
            _context.Genres.Remove(entity);
            _context.SaveChanges();
        }

        return RedirectToAction("GenreList");
    }

    [HttpPost]
    public IActionResult DeleteMovie(int? MovieId)
    {
        var entity = _context.Movies.Find(MovieId);
        if (entity != null)
        {
            _context.Movies.Remove(entity);
            _context.SaveChanges();
        }

        return RedirectToAction("MovieList");
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
    public IActionResult UpdateGenre(AdminGenreEditViewModel model, int[]? movieIds)
    {
        if (model.Movies != null)
        {

            for (var i = 0; i < model.Movies.Count; i++)
            {
                ModelState.Remove($"Movies[{i}].Genres");
            }
        }
        if (ModelState.IsValid)
        {




            var entity = _context.Genres.Include(m => m.Movies).FirstOrDefault(m => m.GenreId == model.GenreId);
            //  if (entity == null) return NotFound();
            entity.Name = model.Name;
            foreach (var id in movieIds)
            {
                entity.Movies.Remove(entity.Movies.FirstOrDefault(m => m.MovieId == id));
            }
            _context.SaveChanges();
            return RedirectToAction("GenreList");
        }
        var errors = ModelState;

        foreach (var entry in errors)
        {
            System.Console.WriteLine("Key: " + entry.Key);  // Property adı (örneğin: Title, Description)
            foreach (var item in entry.Value.Errors)
            {
                System.Console.WriteLine("Error Message: " + item.ErrorMessage);  // Hata mesajı
            }
        }

        return View(model);
    }




}