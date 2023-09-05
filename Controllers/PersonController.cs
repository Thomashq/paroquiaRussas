using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public PersonController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public List<Person> GetAllPerson()
        {
            return _appDbContext.Person.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetpersonById(long id)
        {
            try
            {
                return _appDbContext.Person.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar usuário pelo Id.", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Addperson(Person person)
        {
            try
            {
                PasswordEncryption passwordEncryption = new PasswordEncryption(person.Pwd);
                person.Pwd = passwordEncryption.Encrypt(person.Pwd);

                _appDbContext.Add(person);
                await _appDbContext.SaveChangesAsync();

                return Ok("Usuário adicionado com sucesso");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar usuário", ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(long id)
        {
            try
            {
                Person person = _appDbContext.Person.FirstOrDefault(x => x.Id == id);

                if (person != null)
                {
                    _appDbContext.Person.Remove(person);
                    await _appDbContext.SaveChangesAsync();
                }

                return Ok("Usuário Deletado com Sucesso");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}