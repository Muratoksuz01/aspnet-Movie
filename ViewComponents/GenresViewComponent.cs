using Microsoft.AspNetCore.Mvc;
using movieApp_Web.Data;
using movieApp_Web.Models;
using System.Collections.Generic;

namespace movieApp_Web.ViewComponents
{
  public class GenresViewComponent : ViewComponent
  {

    private readonly MovieContext _context;

    public GenresViewComponent(MovieContext context)
    {
      _context = context;
    }
    public IViewComponentResult Invoke()
    {
      ViewBag.SelectId = RouteData.Values["id"];
      return View(_context.Genres.ToList());
    }
  }
}