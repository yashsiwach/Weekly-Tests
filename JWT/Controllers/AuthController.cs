using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
