using System.ComponentModel.DataAnnotations;
using movieApp_Web.Models;

namespace movieApp_Web.Validators
{
    public class ClassicMovieAttribute:ValidationAttribute
    {
        public ClassicMovieAttribute(int year)
        {
            Year=year;
        }
        public int Year { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var movie=(AdminCreateViewModel)validationContext.ObjectInstance;
            var releaseYear=((DateTime)value).Year;
            if(movie.IsClassic &&releaseYear>Year){
                return new ValidationResult($"clasik film icin {Year} ve oncesi deger girmelisiniz " );
            }
            return ValidationResult.Success;
        }

    }
}