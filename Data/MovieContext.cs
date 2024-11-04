using Microsoft.EntityFrameworkCore;
using movieApp_Web.Entity;

namespace movieApp_Web.Data
{
    public class MovieContext : DbContext 
    {

        public MovieContext(DbContextOptions<MovieContext> options):base(options)
        {
            
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Cast> Casts { get; set; }


        // bu kullanılabirl ama solid degil burası varsa progream cs ve applicaton.json kısmı olmayacak 
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=localhost;Database=moviesdb;User='root';password=''; Integrated Security-True");
        // }
    }

}