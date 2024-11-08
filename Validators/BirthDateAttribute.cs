using System.ComponentModel.DataAnnotations;

namespace movieApp_Web.Validators
{
    public class BirthDateAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime datetime=Convert.ToDateTime(value);
            return datetime <=DateTime.Now;
        }
    }
    
}