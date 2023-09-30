using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Resources;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public NewsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Route("View")]
        public IActionResult Index()
        {
            List<News> newsList = GetNews();
            newsList.Sort((n1, n2) => n2.CreationDate.CompareTo(n1.CreationDate));

            return View(newsList);
        }

        [HttpGet]
        public List<News> GetNews()
        {
            try
            {
                NewsRepository newsRepository = new NewsRepository(_appDbContext);
                List<News> newsList = newsRepository.GetNews();

                return newsList;
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC10, ex);
            }
        }

        [HttpGet("{id}")]
        public News GetNewsById(long id)
        {
            try
            {
                NewsRepository newsRepository = new NewsRepository(_appDbContext);
                News news = newsRepository.GetNewsById(id);

                return news;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Exceptions.EXC11, id));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewNews([FromForm] News news)
        {
            try
            {
                NewsRepository newsRepository = new NewsRepository(_appDbContext);
                var result = newsRepository.CreateNewNews(news);

                await _appDbContext.SaveChangesAsync();

                TempData["SucessMessage"] = Messages.MSG05;
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = Exceptions.EXC12;
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpPut]
        public IActionResult UpdateNews(News news)
        {
            try
            {
                NewsRepository newsRepository = new NewsRepository(_appDbContext);

                News newsToEdit = newsRepository.UpdateNews(news);

                if (newsToEdit == null)
                    return NotFound(string.Format(Exceptions.EXC11, news.Id));

                _appDbContext.SaveChanges();

                return Ok(Messages.MSG06);
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC13, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<News>> DeleteNews(long id)
        {
            try
            {
                NewsRepository newsRepository = new NewsRepository(_appDbContext);
                News newsToDelete = newsRepository.DeleteNews(id);

                if (newsToDelete == null)
                    return NotFound(string.Format(Exceptions.EXC11, id));

                _appDbContext.SaveChangesAsync();

                return Ok(Messages.MSG07);
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC14, ex);
            }
        }

        [Route("View/{id}")]
        public IActionResult OpenModalNews(long id)
        {
            try
            {
                News news = new News();

                news = GetNewsById(id);

                return Json(new { data = news });
            }
            catch(Exception ex)
            {
                return Json(new { error = Messages.MSG13 });
            }
        }
    }
}
