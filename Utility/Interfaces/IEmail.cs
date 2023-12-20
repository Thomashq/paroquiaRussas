using paroquiaRussas.Models;

namespace paroquiaRussas.Utility.Interfaces
{
    public interface IEmail
    {
        bool Send(Mail mail);
    }
}
