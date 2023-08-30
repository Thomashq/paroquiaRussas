using Microsoft.AspNetCore.Mvc;

namespace paroquiaRussas.Controllers
{
    public class LiturgyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
