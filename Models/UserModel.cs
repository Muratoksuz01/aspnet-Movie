using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using movieApp_Web.Validators;

namespace movieApp_Web.Models
{
    
    public  class UserModel{
        public int UserId { get; set; }
        [Required]
        [StringLength(10,ErrorMessage ="Username icin 10 karakterden fazla olamaz")]
        [Remote(action:"VerifyUserName",controller:"User")]
        public string UserName { get; set; }
        [Required]
        [StringLength(15,ErrorMessage ="{0} karakter uzunlugu {2}-{1} arasında olmalıdır",MinimumLength =3)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [EmailProvider]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
        [Url]
        public string Url { get; set; }
        // [Range(1990,2024)]
        // public int BirthYear { get; set; }
        [BirthDate(ErrorMessage ="tarih bugun yada sonraki tarih olamaz ")]
        [DataType(DataType.Date)]
        [Display(Name ="Birth Date")]
        public DateTime BirthDate{get;set;}
    }
}