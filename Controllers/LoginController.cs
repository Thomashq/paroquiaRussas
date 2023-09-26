using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
using paroquiaRussas.Services;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Interfaces;
using paroquiaRussas.Utility.Resources;
using System.Security.Claims;
using Enum = paroquiaRussas.Utility.Enum;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        private readonly IToken _token;

        public LoginController(AppDbContext appDbContext, IConfiguration configuration, IToken token)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
            _token = token;
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
                    return NotFound(new { message = Exceptions.EXC08 });

                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, person.Id.ToString()),
                     new Claim(ClaimTypes.Role, Enum.GetEnumDescription(person.Role))
                };

                _token.GenerateToken(claims);

                return Ok(new {message = Messages.MSG04});
            }
            catch(Exception ex) 
            {
                throw new Exception(Exceptions.EXC09, ex);
            }
        }
    }
}
