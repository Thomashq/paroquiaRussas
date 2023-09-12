using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Utility.Interfaces;

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
                    return BadRequest();

                return Ok(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível mandar o email", ex);
            }
        }
    }
}
