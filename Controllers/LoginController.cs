using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
using paroquiaRussas.Services;
using paroquiaRussas.Utility;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public LoginController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
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
                PersonRepository personRepository = new PersonRepository(_appDbContext);

                person = personRepository.GetPersonToLogin(person.Username, person.Pwd);

                if (person == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                TokenServices tokenServices = new TokenServices(_configuration);
                var token = tokenServices.GenerateToken(person);

                return Ok(new {message = "Usuário logado com sucesso"});
            }
            catch(Exception ex) 
            {
                throw new Exception("Não foi possível efetuar login.", ex);
            }
        }
    }
}
