using paroquiaRussas.Models;

namespace paroquiaRussas.Utility.Interfaces
{
    public interface IToken
    {
        string GenerateToken(Person person);
    }
}
