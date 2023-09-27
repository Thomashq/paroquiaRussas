using Microsoft.AspNetCore.Mvc;

namespace paroquiaRussas.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
