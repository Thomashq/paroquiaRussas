using Microsoft.AspNetCore.Identity;
using static paroquiaRussas.Utility.Enum;

namespace paroquiaRussas.Models;
public class Users : BaseModel
{
    public Role Role{ get; set; }

    public string Username { get; set; }

    public string Pwd { get; set; }
}
