using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Resources;
using System;

namespace paroquiaRussas.Repository
{
    public class PersonRepository
    {
        private readonly AppDbContext _appDbContext;

        public PersonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC20, ex);
            }
        }

        public Person GetPersonById(long id)
        {
            try
            {
                return _appDbContext.Person.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Exceptions.EXC11, id), ex);
            }
        }

        public List<Person> GetAll()
        {
            try
            {
                return _appDbContext.Person.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC15, ex);
            }
        }

        public ActionResult<dynamic> CreateNewPerson(Person person)
        {
            try
            {
                PasswordEncryption passwordEncryption = new PasswordEncryption(person.Pwd);
                person.Pwd = passwordEncryption.Encrypt(person.Pwd);

                person.CreationDate = DateOnly.FromDateTime(DateTime.Now);

                _appDbContext.Add(person);

                return person;
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC16, ex);
            }
        }

        public ActionResult<dynamic> DeletePerson(long id)
        {
            try
            {
                Person personToDelete = _appDbContext.Person.FirstOrDefault(x => x.Id == id);

                if (personToDelete != null)
                {
                    _appDbContext.Person.Remove(personToDelete);
                    return Messages.MSG10; 
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC17, ex);
            }
        }
    }
}
