using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace movieApp_Web.Models
{
    public class Movie
    {    // buradaki tum attribute ezberleme 'data annotations '
        public int MovieId { get; set; }
        [DisplayName("Başlık")]// bu ozellik create.cshtml yeni verisionda kullanılır daha cok 
        [Required]
        public string? Title { get; set; }
        [Required(ErrorMessage ="acıklama girmelisiniz!!!")]
        public string? Description { get; set; }
        [StringLength(50,MinimumLength =5,ErrorMessage ="5 max 50 karakter girmelisin ")]
        [Required]
        public string? Director { get; set; }
        [Required]
        public string[]? Players { get; set; }
       [Required]
        public string? Imageurl { get; set; }
        [Required]
        public int GenreId { get; set; }
    }
}