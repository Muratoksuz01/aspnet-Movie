using System.ComponentModel;

namespace movieApp_Web.Models
{
    public class Movie
    {   
        public int MovieId { get; set; }
        [DisplayName("Başlık")]// bu ozellik create.cshtml yeni verisionda kullanılır daha cok 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public string[]? Players { get; set; }
        public string? Imageurl { get; set; }
        public int GenreId { get; set; }
    }
}