using System.ComponentModel.DataAnnotations;

namespace movieApp_Web.Models
{
    public class AdminGenresViewModel
    {
        [Required(ErrorMessage ="tur ismi girmelisiniz ")]
        public string Name { get; set; }
        public List<AdminGenreViewModel> Genres { get; set; }

    }
   
    public class AdminGenreViewModel
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class AdminGenreEditViewModel
    {
        public int GenreId { get; set; }
        [Required(ErrorMessage ="tur ismi bos gecemen")]
        public string Name { get; set; }
        public List<AdminMoviesViewModels> Movies { get; set; }
    }
}