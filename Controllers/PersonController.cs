using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
using paroquiaRussas.Utility;
using System;
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
            try
            {
                PersonRepository personRepository = new PersonRepository(_appDbContext);

                return personRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception();
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
                throw new Exception("Erro ao recuperar usuário pelo Id.", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Addperson(Person person)
        {
            try
            {
                PersonRepository personRepository = new PersonRepository(_appDbContext);
                personRepository.CreateNewPerson(person);

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
                PersonRepository personRepository = new PersonRepository(_appDbContext);
                var result = personRepository.DeletePerson(id);

                if (result == null)
                    return BadRequest();

                await _appDbContext.SaveChangesAsync();
                return Ok("Usuário Deletado com Sucesso");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}