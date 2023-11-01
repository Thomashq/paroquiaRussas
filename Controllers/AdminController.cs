using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Resources;

namespace paroquiaRussas.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AdminController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            try
            {
                AdminModel adminModel = new AdminModel();

                adminModel.EventList = new EventsRepository(_appDbContext).GetEvents();

                adminModel.NewsList = new NewsRepository(_appDbContext).GetNews();

                adminModel.PersonList = new PersonRepository(_appDbContext).GetAll();

                return View(adminModel);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = Exceptions.EXC25;
                return View();
            }
        }
    }
}
