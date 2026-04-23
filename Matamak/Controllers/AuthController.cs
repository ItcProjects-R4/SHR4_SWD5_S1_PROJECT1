using Microsoft.AspNetCore.Mvc;

namespace Matamak.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
