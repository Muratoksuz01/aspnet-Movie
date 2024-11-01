

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
            Title="The Silent Storm",
            Description="A gripping drama about a young woman finding her voice amidst family struggles.",
            Director="Olivia Brown",
            Players=new string[]{ "Emma Stone","Ryan Gosling"},
            Imageurl="film1.jpg",
            GenreId=3
        },
        new Movie{
            MovieId=2,
            Title="Echoes of Eternity",
            Description="A suspense thriller that dives into the mysterious disappearance of a scientist.",
            Director="James Black",
            Players=new string[]{ "Robert Downey Jr.","Scarlett Johansson"},
            Imageurl="film2.jpg",
            GenreId=2
        },
        new Movie{
            MovieId=3,
            Title="The Lost Kingdom",
            Description="An epic adventure of a prince seeking his lost homeland across mystical lands.",
            Director="Steven Chan",
            Players=new string[]{ "Chris Hemsworth","Zoe Saldana"},
            Imageurl="film3.jpg",
            GenreId=1
        },
        new Movie{
            MovieId=4,
            Title="Autumn's Embrace",
            Description="A heartfelt romance that explores love and loss in a small coastal town.",
            Director="Maria Lopez",
            Players=new string[]{ "Anne Hathaway","Jake Gyllenhaal"},
            Imageurl="film4.jpg",
            GenreId=4
        },
        new Movie{
            MovieId=5,
            Title="Beyond the Horizon",
            Description="An inspiring story of an astronaut's journey back to Earth after a failed mission.",
            Director="Daniel Lee",
                       Players=new string[]{ "Tom Hanks","Sandra Bullock"},
            Imageurl="film1.jpg",
            GenreId=2
        },
        new Movie{
            MovieId=6,
            Title="Legacy of the Brave",
            Description="A historical epic chronicling the life of a legendary war hero.",
            Director="Christopher Scott",
            Players=new string[]{ "Russell Crowe","Cate Blanchett"},
            Imageurl="film2.jpg",
            GenreId=1
        },
        new Movie{
            MovieId=7,
            Title="Whispers in the Dark",
            Description="A horror film that unveils the haunting secrets of an old mansion.",
            Director="Linda Roberts",
            Players=new string[]{ "Emily Blunt","John Krasinski"},
            Imageurl="film3.jpg",
            GenreId=5
        },
        new Movie{
            MovieId=8,
            Title="City Lights",
            Description="A documentary following the life of street artists in the heart of New York City.",
            Director="Michael Adams",
            Players=new string[]{ "Various Artists"},
            Imageurl="film4.jpg",
            GenreId=6
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

        public static void Del(int id)
        {
            _movies.RemoveAll(movie => movie.MovieId == id);
        }
        public static void Edit(Movie m)
        {
            foreach (var movie in _movies)
            {
                if (movie.MovieId == m.MovieId)
                {
                    movie.Title = m.Title;
                    movie.Description = m.Description;
                    movie.Director = m.Director;
                    movie.Imageurl = m.Imageurl;
                    movie.GenreId = m.GenreId;
                    break;
                }
            }
        }
        public static void Add(Movie movie)
        {
            movie.MovieId = _movies.Count() + 1;
            _movies.Add(movie);
        }
        public static Movie GetById(int id)
        {
            return _movies.FirstOrDefault(m => m.MovieId == id);
        }
    }
}