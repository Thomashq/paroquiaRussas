using Microsoft.AspNetCore.Mvc;

namespace paroquiaRussas.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
