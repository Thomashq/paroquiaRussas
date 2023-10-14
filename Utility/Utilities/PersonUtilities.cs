using paroquiaRussas.Models;
using paroquiaRussas.Repository;

namespace paroquiaRussas.Utility.Utilities
{
    public class PersonUtilities
    {
        public static bool ValidateExistingPerson(Person person, PersonRepository personRepository)
        {
            Person existingPerson = personRepository.GetPersonToLogin(person.Username, person.Pwd);

            if (existingPerson != null)
                return true;

            return false;
        }
    }
}
