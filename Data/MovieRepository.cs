

using System.Reflection.Metadata.Ecma335;
using movieApp_Web.Models;

namespace movieApp_Web.Data
{
    public class MovieRapository
    {
        private static readonly List<Movie> _movies = null;
        static MovieRapository()
        {
            _movies = new List<Movie>(){
            new Movie{
                MovieId=1,
                Title="Film 1",
                Description="acıklama 1",
                Director="Yonetmen",
                Players=new string[]{ "oyuncu 1","oyuncu 2"},
                Imageurl="film1.jpg",
                GenreId=3
            },
            new Movie{
                MovieId=2,
                Title="Film 2",
                Description="acıklama acıklama3-2 1",
                Director="Yonetmen",
                Players=new string[]{ "oyuncu 1","oyuncu 2"},
                Imageurl="film2.jpg",
                GenreId=3
            },
            new Movie{
                MovieId=3,
                Title="Film 3",
                Description="acıklama acıklama3 1",
                Director="Yonetmen",
                Players=new string[]{ "oyuncu 1","oyuncu 2"},
                Imageurl="film3.jpg",
                GenreId=2
            },
            new Movie{
                MovieId=4,
                Title="Film 4",
                Description="acıklama acıklama5 1",
                Director="Yonetmen",
                Players=new string[]{ "oyuncu 1","oyuncu 2"},
                Imageurl="film4.jpg",
                GenreId=1
            },
            new Movie{
                MovieId=5,
                Title="Film 1",
                Description="acıklama 1",
                Director="Yonetmen",
                Players=new string[]{ "oyuncu 1","oyuncu 2"},
                Imageurl="film1.jpg",
                GenreId=1
            },
            new Movie{
                MovieId=6,
                Title="Film 2",
                Description="acıklama 1",
                Director="Yonetmen",
                Players=new string[]{ "oyuncu 1","oyuncu 2"},
                Imageurl="film2.jpg",
                GenreId=1
            },
            new Movie{
                MovieId=7,
                Title="Film 3",
                Description="acıklama 1",
                Director="Yonetmen",
                Players=new string[]{ "oyuncu 1","oyuncu 2"},
                Imageurl="film3.jpg",
                GenreId=2
            },
            new Movie{
                MovieId=8,
                Title="Film 4",
                Description="acıklama 1",
                Director="Yonetmen",
                Players=new string[]{ "oyuncu 1","oyuncu 2"},
                Imageurl="film4.jpg",
                GenreId=3
            }


        };
        }
        public static List<Movie> Movies
        {
            get
            {
                return _movies;
            }
        }
        public static void Add(Movie movie)
        {   
            movie.MovieId=_movies.Count()+1;
            _movies.Add(movie);
        }
        public static Movie GetById(int id)
        {
            return _movies.FirstOrDefault(m => m.MovieId == id);
        }
    }
}