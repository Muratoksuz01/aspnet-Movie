
using movieApp_Web.Entity;

namespace movieApp_Web.Models
{
    public class AdminMoviesViewModel
    {
        public List<AdminMoviesViewModels> Movies { get; set; }
    }
    public class AdminMoviesViewModels
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public List<Genre> Genres { get; set; }

    }
    public class AdminMoviesModelEdit
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<Genre> SelectedGenres { get; set; }

    }
}
