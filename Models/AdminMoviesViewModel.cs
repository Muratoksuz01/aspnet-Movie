using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using movieApp_Web.Entity;
using movieApp_Web.Validators;

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

        [Display(Name = "Film Adı")]
        [Required(ErrorMessage = "mesaj baslıgı girmelisiniz ")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "mesaj acıklama girmelisiniz  ")]
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Lütfen bir tur seçin.")]
        public int[] GenreIds { get; set; }

    }

    public class AdminCreateViewModel
    {
        [Display(Name = "Film Adı")]
        [Required(ErrorMessage = "mesaj baslıgı girmelisiniz ")]
        public string Title { get; set; }
        [Required(ErrorMessage = "mesaj acıklama girmelisiniz  ")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Lütfen bir dosya seçin.")]
        public IFormFile file { get; set; }
        [Required(ErrorMessage = "Lütfen bir tur seçin.")]
        public int[] GenreIds { get; set; }
        public bool IsClassic { get; set; }
        [ClassicMovie(1950)]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate {get;set;} =DateTime.Now;

    }

}
