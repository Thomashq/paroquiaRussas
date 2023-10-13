using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;

namespace paroquiaRussas.Controllers
{
    [Route("Error")]
    [ApiController]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = HttpContext.Response.StatusCode,
                ErrorMessage = feature?.Error.Message
            };

            return View(errorViewModel);
        }
    }
}
