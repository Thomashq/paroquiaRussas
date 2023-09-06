using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;

namespace paroquiaRussas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public EventsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Route("View")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<Event> GetAllEvents()
        {
            return _appDbContext.Event.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Event eventToPost)
        {
            try
            {
                _appDbContext.Add(eventToPost);
                await _appDbContext.SaveChangesAsync();

                return Ok("Evento adicionado com sucesso");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar evento", ex);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEventById(int id)
        {
            try
            {
                return _appDbContext.Event.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao encontrar evento", ex);
            }
        }

        [HttpGet("GetEventByDate/{date}")]
        public List<Event> GetEventByDate(string date)
        {
            try
            {
                DateOnly dateOnly = DateOnly.Parse(date);
                return _appDbContext.Event.Where(x => x.EventDate == dateOnly).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar evento por data", ex);
            }
        }

        [HttpPut]
        public IActionResult EditEvent(Event eventUpdate)
        {
            try
            {
                Event eventToEdit = _appDbContext.Event.FirstOrDefault(x => x.Id == eventUpdate.Id);

                if (eventToEdit == null)
                    return NotFound(); // Retorna 404 caso o evento não seja encontrado

                if (eventUpdate.EventDate != null)
                    eventToEdit.EventDate = eventUpdate.EventDate;

                if (!string.IsNullOrEmpty(eventUpdate.EventDescription))
                    eventToEdit.EventDescription = eventUpdate.EventDescription;

                if (!string.IsNullOrEmpty(eventUpdate.EventImage))
                    eventToEdit.EventImage = eventUpdate.EventImage;

                if (!string.IsNullOrEmpty(eventUpdate.EventName))
                    eventToEdit.EventName = eventUpdate.EventName;

                eventToEdit.UpdateDate = DateOnly.FromDateTime(DateTime.Now);

                _appDbContext.Event.Update(eventToEdit);
                _appDbContext.SaveChanges();

                return Ok(eventToEdit);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar evento", ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(long id)
        {
            try
            {
                Event eventToDelete = await _appDbContext.Event.FirstOrDefaultAsync(x => x.Id == id);

                if (eventToDelete == null)
                    return NotFound();

                _appDbContext.Event.Remove(eventToDelete);

                await _appDbContext.SaveChangesAsync();

                return Ok("Evento excluído com sucesso");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir evento", ex);
            }
        }
    }
}
