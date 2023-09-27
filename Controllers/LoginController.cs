using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
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

        [Route("View")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Person person)
        {
            try
            {
                LoginModel loginModel = await AuthenticateAsync(person);

                if (loginModel.Status == StatusCodes.Status404NotFound)
                {
                    TempData["ErrorMessage"] = loginModel.Message;
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public async Task<LoginModel> AuthenticateAsync([FromBody] Person person)
        {
            try
            {
                PersonRepository personRepository = new PersonRepository(_appDbContext);

                person = personRepository.GetPersonToLogin(person.Username, person.Pwd);

                if (person == null)
                    return new LoginModel { Message = Exceptions.EXC08, Status = 404 };

                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, person.Id.ToString()),
                     new Claim(ClaimTypes.Role, Enum.GetEnumDescription(person.Role))
                };

                var token = await _token.GenerateToken(claims);

                return new LoginModel { Message = Messages.MSG04, Status = 200 };
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC09, ex);
            }
        }
    }
}
