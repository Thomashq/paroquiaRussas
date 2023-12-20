using Microsoft.AspNetCore.Mvc;

namespace paroquiaRussas.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
