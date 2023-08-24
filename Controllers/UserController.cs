using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using static paroquiaRussas.Utility.Enum;

namespace paroquiaRussas.Controllers;

[ApiController]
[Route("api/admin")]
public class UserController : Controller
{
    private readonly AppDbContext _appDbContext;

    public UserController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public List<Users> GetAllUsers(){
        return _appDbContext.Users.ToList();
    }

    [HttpGet]
    [Route("getbyid")] 
    public Users GetUserById(int userId)
    {
        try
        {
            Users user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);

            return user != null ? user : null;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUser(Users user)
    {
        try{           
            
            //if(role != Role.Admin)
                //BadRequest("Você não tem permissão para adicionar um usuário");
            
            PasswordEncryption passwordEncryption = new PasswordEncryption(user.Pwd);
            user.Pwd = passwordEncryption.Encrypt(user.Pwd);
            _appDbContext.Add(user);
            await _appDbContext.SaveChangesAsync();
            
            return Ok("Usuário adicionado com sucesso");           
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        try
        {
            Users user = userId > 0 ? _appDbContext.Users.Find(userId) : throw new Exception();

            if(user != null){
                _appDbContext.Users.Remove(user);
                await _appDbContext.SaveChangesAsync();
            }

            return Ok("Usuário Deletado com Sucesso");
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
