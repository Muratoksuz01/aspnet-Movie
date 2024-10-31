using Microsoft.AspNetCore.Mvc;
using movieApp_Web.Data;
using movieApp_Web.Models;
using System.Collections.Generic;

namespace movieApp_Web.ViewComponents
{
    public class GenresViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
          ViewBag.SelectId=RouteData.Values["id"];
        return View(GenreRepository.Genres);
        }
    }
}