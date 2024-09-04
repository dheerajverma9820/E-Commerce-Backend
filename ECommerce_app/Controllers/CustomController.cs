//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace ECommerce_app.Controllers
//{
//    public class BaseApiController : Controller
//    {
//        protected string GetUserEmail()
//        {
//            return User.FindFirstValue(ClaimTypes.Email);
//            var userId = User.Claims.ToList()[2]?.Value;
//            var email = User.FindFirst(ClaimTypes.Email)?.Value;
//            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            var firstName = User.FindFirst("first_name")?.Value;
//            var lastName = User.FindFirst("last_name")?.Value;
//            var gender = User.FindFirst(ClaimTypes.Gender)?.Value;
//        }

//    }
//}
