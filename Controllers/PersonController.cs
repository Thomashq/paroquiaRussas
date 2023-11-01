using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Resources;
using paroquiaRussas.Utility.Utilities;
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

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Addperson([FromForm] Person person)
        {
            try
            {
                PersonRepository personRepository = new PersonRepository(_appDbContext);

                if (PersonUtilities.ValidateExistingPerson(person, personRepository) == true)
                {
                    TempData["ErrorMessage"] = Exceptions.EXC16;
                    return RedirectToAction("Index", "Admin");
                }

                personRepository.CreateNewPerson(person);

                await _appDbContext.SaveChangesAsync();

                TempData["SucessMessage"] = Messages.MSG08;
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = Exceptions.EXC16;
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpDelete("DeletePerson/{id}")]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                PersonRepository personRepository = new PersonRepository(_appDbContext);
                
                var result = personRepository.DeletePerson(id);

                if (result == null)
                    return Json(new { error = Exceptions.EXC17 });

                _appDbContext.SaveChangesAsync();

                return Json(new { message = Messages.MSG09 });
            }
            catch (Exception ex)
            {
                return Json(new { error = Exceptions.EXC17 });
            }
        }
    }
}