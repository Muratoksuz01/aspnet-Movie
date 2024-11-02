using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace movieApp_Web.Entity
{
    public class Movie
    {    // buradaki tum attribute ezberleme 'data annotations ' diye arat 
        [Required]
            public int MovieId { get; set; }
        [Required]
            public string? Title { get; set; }
        [MaxLength(500)]
            public string? Description { get; set; }
        public string? Director { get; set; }
        public string? Imageurl { get; set; }
        [Required]
            public int GenreId { get; set; }
    }
}