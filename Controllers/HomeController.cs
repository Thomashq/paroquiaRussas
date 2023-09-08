using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;

namespace paroquiaRussas.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly AppDbContext _appDbContext;

    public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    public IActionResult Index()
    {
        HomeModel homeModel = new HomeModel();
        List<Event> eventList = _appDbContext.Event.ToList();

        if (eventList.Count > 2)
        {
            homeModel.Events = eventList.GetRange(eventList.Count - 2, 2);
            return View(homeModel);
        }

        homeModel.Events = eventList;

        return View(homeModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
