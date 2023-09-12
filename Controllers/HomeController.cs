using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
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

        homeModel.Events = GetTheLastEvents();
        homeModel.News = GetTheLastNews();

        return View(homeModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private List<Event> GetTheLastEvents()
    {
        EventsRepository eventsRepository = new EventsRepository(_appDbContext);

        List<Event> eventList = eventsRepository.GetEvents();

        if (eventList.Count > 2)
            return eventList.GetRange(eventList.Count - 2, 2);

        return eventList;
    }

    private List<News> GetTheLastNews()
    {
        NewsRepository newsRepository = new NewsRepository(_appDbContext);

        List<News> newsList = newsRepository.GetNews();

        if (newsList.Count > 2)
            return newsList.GetRange(newsList.Count - 3, 3);

        return newsList;
    }
}
