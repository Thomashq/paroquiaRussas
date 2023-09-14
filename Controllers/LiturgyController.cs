using Microsoft.AspNetCore.Mvc;
using paroquiaRussas.Mapper;
using paroquiaRussas.Models;
using paroquiaRussas.Models.Json;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Factory.LiturgyFactory;
using paroquiaRussas.Utility.Factory.LiturgyFactory.Interface;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiturgyController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public LiturgyController(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _configuration = config;
        }

        [Route("View")]
        public async Task<IActionResult> Index()
        {
            var dailyLiturgy = await GetDailyLiturgy();

            LiturgyModel liturgyModel = LiturgyMapper.LiturgyJsonToModel(dailyLiturgy);

            return View(liturgyModel);
        }

        [HttpGet]
        public async Task<dynamic> GetDailyLiturgy()
        {
            try
            {
                LiturgyApiConfig liturgy = new LiturgyApiConfig();

                liturgy.ApiUrl = _configuration.GetValue<string>("LiturgyApiConfig:ApiUrl");

                HttpResponseMessage response = await _httpClient.GetAsync(liturgy.ApiUrl);

                if (!response.IsSuccessStatusCode)
                    return BadRequest("Não foi possível acessar a liturgia diária.");

                DayOfWeek day = DateTime.Now.DayOfWeek;

                string jsonResult = await response.Content.ReadAsStringAsync();

                ILiturgyInterface liturgyFactory;

                if (day == DayOfWeek.Sunday)
                {
                    liturgyFactory = new SundayLiturgyFactory();
                    SundayLiturgyJson sundayLiturgy = liturgyFactory.CreateSundayLiturgy(jsonResult);

                    return sundayLiturgy;
                }

                liturgyFactory = new WeekLiturgyFactory();

                WeekLiturgyJson weekLiturgyJson = liturgyFactory.CreateWeeklyLiturgy(jsonResult);

                return weekLiturgyJson;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
