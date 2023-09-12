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

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult SendMail(Mail mail)
        {
            try
            {
                bool result = _email.Send(mail);

                if (result == false)
                    return BadRequest(Exceptions.EXC01);

                return Ok(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC01, ex);
            }
        }
    }
}
