using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace paroquiaRussas.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
