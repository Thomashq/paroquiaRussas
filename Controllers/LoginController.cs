using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Services;
using paroquiaRussas.Utility;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public LoginController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] Person person)
        {
            try
            {
                PersonController personController = new(_appDbContext);

                person = personController.GetPersonToLogin(person.Username, person.Pwd);

                if (person == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = TokenServices.GenerateToken(person);

                return Ok(new {message = "Usuário logado com sucesso"});
            }
            catch(Exception ex) 
            {
                throw new Exception("Não foi possível efetuar login.", ex);
            }
        }
    }
}
