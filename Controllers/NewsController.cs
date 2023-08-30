using Microsoft.AspNetCore.Mvc;

namespace paroquiaRussas.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
