using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using System.Runtime.CompilerServices;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            try
            {
                Person person = _appDbContext.Person.FirstOrDefault(x => x.Id == id);

                if (person == null)
                    return NotFound();
                                
                _appDbContext.Person.Remove(person);
                
                await _appDbContext.SaveChangesAsync();

                return Ok("Usuário Deletado com Sucesso");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Person GetPersonToLogin(string userName, string password)
        {
            try
            {
                Person person = new();
                PasswordEncryption passwordEncryption = new(password);

                password = passwordEncryption.Encrypt(password);

                person = _appDbContext.Person.FirstOrDefault(a => a.Username == userName && a.Pwd == password);

                return person;
            }
            catch(Exception ex)
            {
                throw new Exception("Ocorreu um erro ao encontrar o usuário", ex);
            }
        }

    }
}