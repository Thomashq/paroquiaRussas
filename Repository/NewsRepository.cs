using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Resources;
using paroquiaRussas.Utility.Utilities;

namespace paroquiaRussas.Repository
{
    public class NewsRepository
    {
        private readonly AppDbContext _appDbContext;

        public NewsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<News> GetNews()
        {
            try
            {
                return _appDbContext.News.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC12, ex);
            }
        }

        public News GetNewsById(long id)
        {
            try
            {
                return _appDbContext.News.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Exceptions.EXC10, id), ex);
            }
        }

        public News CreateNewNews(News news)
        {
            try
            {
                news.CreationDate = DateOnly.FromDateTime(DateTime.Now);
                news.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
                news.NewsImage = ImagesManagement.SaveImage(news.NewsImage);

                _appDbContext.News.Add(news);

                return news;
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC12, ex);
            }
        }

        public News UpdateNews(News news)
        {
            try
            {
                News newsToEdit = _appDbContext.News.FirstOrDefault(x => x.Id == news.Id);

                if (newsToEdit == null)
                    return null;

                //só faz as trocas de valores se os membros do objeto news forem diferentes de nulo
                newsToEdit.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
                newsToEdit.NewsTitle = news.NewsTitle ?? newsToEdit.NewsTitle;
                newsToEdit.Headline = news.Headline ?? newsToEdit.Headline;
                newsToEdit.NewsContent = news.NewsContent ?? newsToEdit.NewsContent;

                if(news.NewsImage != newsToEdit.NewsImage)
                    newsToEdit.NewsImage = ImagesManagement.SaveImage(news.NewsImage)  ?? newsToEdit.NewsImage;

                _appDbContext.News.Update(newsToEdit);

                return newsToEdit;
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC13, ex);
            }
        }

        public News DeleteNews(long id)
        {
            try
            {
                News news = _appDbContext.News.FirstOrDefault(x => x.Id == id);

                if (news == null)
                    return null;

                _appDbContext.News.Remove(news);

                return news;
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC14, ex);
            }
        }
    }
}
