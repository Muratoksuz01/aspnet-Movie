using Microsoft.EntityFrameworkCore;
using movieApp_Web.Entity;

namespace movieApp_Web.Data
{
    public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<MovieContext>();
            context.Database.Migrate();

            // Veritabanı daha önce oluşturulmamışsa verileri ekle
            if (!context.Genres.Any() && !context.Movies.Any())
                
            {
                var genres = new List<Genre>(){
                    new Genre{GenreId=1,Name="Macera"},
                    new Genre{GenreId=2,Name="Komedi"},
                    new Genre{GenreId=3,Name="Romantik"},
                    new Genre{GenreId=4,Name="Savas"},
                };

                var movies = new List<Movie>(){
                    new Movie{
                        MovieId=1,
                        Title="The Silent Storm",
                        Description="A gripping drama about a young woman finding her voice amidst family struggles.",
                        Imageurl="film1.jpg",
                        Genres=new List<Genre>() {genres[0],genres[1]}
                    },
                    new Movie{
                        MovieId=2,
                        Title="Echoes of Eternity",
                        Description="A suspense thriller that dives into the mysterious disappearance of a scientist.",
                        Imageurl="film2.jpg",
                        Genres=new List<Genre>() {genres[0],genres[1]}
                    },
                    new Movie{
                        MovieId=3,
                        Title="The Lost Kingdom",
                        Description="An epic adventure of a prince seeking his lost homeland across mystical lands.",
                        Imageurl="film3.jpg",
                        Genres=new List<Genre>() {genres[2],genres[3]}
                    },
                    new Movie{
                        MovieId=4,
                        Title="Autumn's Embrace",
                        Description="A heartfelt romance that explores love and loss in a small coastal town.",
                        Imageurl="film4.jpg",
                        Genres=new List<Genre>() {genres[1],genres[2]}
                    },
                    new Movie{
                        MovieId=5,
                        Title="Beyond the Horizon",
                        Description="An inspiring story of an astronaut's journey back to Earth after a failed mission.",
                        Imageurl="film1.jpg",
                        Genres=new List<Genre>() {genres[3],genres[1]}
                    },
                    new Movie{
                        MovieId=6,
                        Title="Legacy of the Brave",
                        Description="A historical epic chronicling the life of a legendary war hero.",
                        Imageurl="film2.jpg",
                        Genres=new List<Genre>() {genres[2],genres[1]}
                    }
                };

                var users = new List<User>(){
                    new User(){Username="usera",Email="usera@gmail.com",Password="1234",ImageUrl="person1.jpg"},
                    new User(){Username="userb",Email="userb@gmail.com",Password="1234",ImageUrl="person2.jpg"},
                    new User(){Username="userc",Email="userc@gmail.com",Password="1234",ImageUrl="person3.jpg"},
                    new User(){Username="user4",Email="user4@gmail.com",Password="1234",ImageUrl="person4.jpg"}
                };

                var people = new List<Person>(){
                    new Person(){
                        Name="personal 2",
                        Biography="tanıtım 2",
                        HomePage="www.2.com",
                        User=users[1]
                    },
                    new Person(){
                        Name="personal 1",
                        Biography="tanıtım 1",
                        HomePage="www.1.com",
                        User=users[0]
                    }
                };

                var crews = new List<Crew>(){
                    new Crew(){Movie=movies[0],Person=people[0],Job="yonetmen"},
                    new Crew(){Movie=movies[0],Person=people[1],Job="yonetmen Yard."}
                };

                var cast = new List<Cast>(){
                    new Cast(){Movie=movies[0],Person=people[0],Name="oyuncu adı 1",Character="karakter 1"},
                    new Cast(){Movie=movies[0],Person=people[1],Name="oyuncu adı 2",Character="karakter 2"}
                };

                context.Genres.AddRange(genres);
                context.Movies.AddRange(movies);
                context.Users.AddRange(users);
                context.People.AddRange(people);
                context.Crews.AddRange(crews);
                context.Casts.AddRange(cast);
                context.SaveChanges();
            }
        }
    }
}
