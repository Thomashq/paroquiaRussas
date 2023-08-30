using System.Reflection.Metadata;
using paroquiaRussas.Transactional;
using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using static paroquiaRussas.Utility.Enum;

namespace paroquiaRussas.Controllers
{

    [ApiController]
    [Route("api/admin")]
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

        [HttpGet]
        [Route("getbyid/{id}")]
        public ActionResult<Person> GetpersonById([FromRoute] long personId)
        {
            try
            {
                PersonTRA personTRA = new(_appDbContext);
                var person = personTRA.GetPersonById(personId);

                return person;
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
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            try
            {
                Person person = personId > 0 ? _appDbContext.Person.FirstOrDefault(x => x.Id == personId) : throw new Exception();

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