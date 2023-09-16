using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Utility.Interfaces;
using paroquiaRussas.Utility.Resources;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IEmail _email;

        public ContactController(IEmail email)
        {
            _email = email;
        }

        [Route("View")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail([FromForm] Mail mail)
        {
            try
            {
                bool result = _email.Send(mail);

                if (result == false)
                {
                    TempData["ErrorMessage"] = Exceptions.EXC01;
                    return RedirectToAction("Index");
                }

                TempData["SucessMessage"] = Messages.MSG12;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC01, ex);
            }
        }
    }
}
