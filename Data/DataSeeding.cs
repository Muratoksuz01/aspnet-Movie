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
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Movies.Count() == 0)
                {
                    context.Movies.AddRange(
                         new List<Movie>(){
                            new Movie{
                                MovieId=1,
                                Title="The Silent Storm",
                                Description="A gripping drama about a young woman finding her voice amidst family struggles.",
                                Imageurl="film1.jpg",
                                GenreId=3
                            },
                            new Movie{
                                MovieId=2,
                                Title="Echoes of Eternity",
                                Description="A suspense thriller that dives into the mysterious disappearance of a scientist.",
                                Imageurl="film2.jpg",
                                GenreId=2
                            },
                            new Movie{
                                MovieId=3,
                                Title="The Lost Kingdom",
                                Description="An epic adventure of a prince seeking his lost homeland across mystical lands.",
                                Imageurl="film3.jpg",
                                GenreId=1
                            },
                            new Movie{
                                MovieId=4,
                                Title="Autumn's Embrace",
                                Description="A heartfelt romance that explores love and loss in a small coastal town.",
                                Imageurl="film4.jpg",
                                GenreId=4
                            },
                            new Movie{
                                MovieId=5,
                                Title="Beyond the Horizon",
                                Description="An inspiring story of an astronaut's journey back to Earth after a failed mission.",
                                Imageurl="film1.jpg",
                                GenreId=2
                            },
                            new Movie{
                                MovieId=6,
                                Title="Legacy of the Brave",
                                Description="A historical epic chronicling the life of a legendary war hero.",
                                Imageurl="film2.jpg",
                                GenreId=1
                            },
                            new Movie{
                                MovieId=7,
                                Title="Whispers in the Dark",
                                Description="A horror film that unveils the haunting secrets of an old mansion.",
                                Imageurl="film3.jpg",
                                GenreId=5
                            },
                            new Movie{
            MovieId=8,
            Title="City Lights",
            Description="A documentary following the life of street artists in the heart of New York City.",
            Imageurl="film4.jpg",
            GenreId=6
        }
                   });
                }
                if(context.Genres.Count()==0){
                    context.AddRange(
                        new List<Genre>(){
                            new Genre{GenreId=1,Name="Macera"},
                            new Genre{GenreId=2,Name="Komedi"},
                            new Genre{GenreId=3,Name="Romantik"},
                            new Genre{GenreId=4,Name="Savas"},
                        }
                    );
                }
           
                context.SaveChanges();
            }
        }
    }


}