using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace movieApp_Web.Entity
{
    public class Movie
    {    // buradaki tum attribute ezberleme 'data annotations '
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public string[]? Players { get; set; }
        public string? Imageurl { get; set; }
        public int GenreId { get; set; }
    }
}