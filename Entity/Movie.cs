using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace movieApp_Web.Entity
{
    public class Movie

    {
            // buradaki tum attribute ezberleme 'data annotations ' diye arat 
            public int MovieId { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Imageurl { get; set; }
        public List<Genre>? Genres { get; set; }
        public Movie()
        {
            Genres = new List<Genre>();
            
        }
    }
}