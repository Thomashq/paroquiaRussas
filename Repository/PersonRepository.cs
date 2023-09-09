using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;
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
                throw new Exception("Ocorreu um erro ao encontrar o usuário", ex);
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
                throw new Exception("Erro ao recuperar usuário pelo Id.", ex);
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
                throw new Exception();
            }
        }

        public ActionResult<dynamic> CreateNewPerson(Person person)
        {
            try
            {
                PasswordEncryption passwordEncryption = new PasswordEncryption(person.Pwd);
                person.Pwd = passwordEncryption.Encrypt(person.Pwd);

                _appDbContext.Add(person);

                return person;
            }
            catch (Exception ex)
            {
                throw new Exception();
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
                    return "Objeto deletado com sucesso"; //substituir a string depois
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
