using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paroquiaRussas.Models.Json;
using paroquiaRussas.Utility;

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
            _httpClient= new HttpClient();
            _configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet] 
        public async Task<ActionResult<LiturgyJson>> GetDailyLiturgy() 
        {
            try
            {
                var liturgy = _configuration.Get<LiturgyApiConfig>();
                string url = liturgy.ApiUrl;

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return BadRequest("Não foi possível acessar a liturgia diária.");

                string jsonResult = await response.Content.ReadAsStringAsync();

                LiturgyJson liturgyJson = JsonConvert.DeserializeObject<LiturgyJson>(jsonResult);

                return liturgyJson;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
