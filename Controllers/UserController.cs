using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using movieApp_Web.Models;

namespace movieApp_Web.Controllers
{

    public class UserController : Controller
    {
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(UserModel m)
        {


            return View();
        }
        [AcceptVerbs("GET","POST")]
        public IActionResult VerifyUserName(string UserName)
        {
            var users = new List<string> { "sadıkturan", "muratoksuz" };
            if (users.Any(i => i == UserName))
            {
                return Json($"zaten bu {UserName} kullanıcı alınmıs ");
            }
            return Json(true);
        }
    }
}