using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Interfaces;
using paroquiaRussas.Utility.Resources;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        private readonly IToken _token;

        public PersonController(AppDbContext appDbContext, IConfiguration configuration, IToken token)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
            _token = token;
        }

        [HttpGet]
        public List<Person> GetAllPerson()
        {
            try
            {
                PersonRepository personRepository = new PersonRepository(_appDbContext);

                return personRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC15, ex);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetpersonById(long id)
        {
            try
            {
                PersonRepository personRepository = new PersonRepository(_appDbContext);

                return personRepository.GetPersonById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Exceptions.EXC11, id), ex);
            }
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Addperson(Person person)
        {
            try
            {
                PersonRepository personRepository = new PersonRepository(_appDbContext);
                personRepository.CreateNewPerson(person);

                await _appDbContext.SaveChangesAsync();

                return Ok(Messages.MSG08);
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC16, ex);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            try
            {
                PersonRepository personRepository = new PersonRepository(_appDbContext);
                var result = personRepository.DeletePerson(id);

                if (result == null)
                    return NotFound(string.Format(Exceptions.EXC11, id));

                await _appDbContext.SaveChangesAsync();

                return Ok(Messages.MSG09);
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC17, ex);
            }
        }
    }
}