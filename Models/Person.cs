using Microsoft.AspNetCore.Identity;
using static paroquiaRussas.Utility.Enum;

namespace paroquiaRussas.Models;
public class Person : BaseModel
{
    public Role Role{ get; set; }

    public string Username { get; set; }

    public string Pwd { get; set; }
}
