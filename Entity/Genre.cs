


using System.ComponentModel.DataAnnotations;

namespace movieApp_Web.Entity
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}