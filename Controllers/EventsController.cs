using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using paroquiaRussas.Repository;
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
            List<Event> eventList = GetAllEvents();

            return View(eventList);
        }

        //public PartialViewResult LoadGridEvents(string searchText)
        //{
        //    List<Event> eventList = GetAllEvents();

        //    var eventListFiltered = eventList.Where(x => !x.EventName.ToLower().Contains(searchText.ToLower()) ||
        //        x.EventDate.ToString("dd/MM/yyyy").Contains(searchText));

        //    return PartialView("_GridEvents", eventListFiltered);
        //}

        [HttpGet]
        public List<Event> GetAllEvents()
        {
            EventsRepository eventsRepository = new EventsRepository(_appDbContext);
            List<Event> events = eventsRepository.GetEvents();

            return events;
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEventById(int id)
        {
            try
            {
                EventsRepository eventsRepository = new EventsRepository(_appDbContext);
                Event eventToGet = eventsRepository.GetEventById(id);

                if (eventToGet == null)
                    return NotFound();

                return eventsRepository.GetEventById(id);
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
                EventsRepository eventsRepository = new EventsRepository(_appDbContext);

                List<Event> eventsToReturn = eventsRepository.GetEventsByDate(date);

                if (eventsToReturn == null)
                    return new List<Event>();

                return eventsToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar evento por data", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Event eventToPost)
        {
            try
            {
                EventsRepository eventsRepository = new EventsRepository(_appDbContext);
                var result = eventsRepository.CreateNewEvent(eventToPost);

                await _appDbContext.SaveChangesAsync();

                return Ok("Evento adicionado com sucesso");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar evento", ex);
            }
        }


        [HttpPut]
        public IActionResult EditEvent(Event eventUpdate)
        {
            try
            {
                EventsRepository eventRepository = new EventsRepository(_appDbContext);
                Event eventToEdit = eventRepository.UpdateEvent(eventUpdate);

                if (eventToEdit == null)
                    return NotFound();

                _appDbContext.SaveChanges();

                return Ok("Evento editado com sucesso");
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
                EventsRepository eventsRepository = new EventsRepository(_appDbContext);
                Event eventToDelete = eventsRepository.DeleteEventById(id);

                if (eventToDelete == null)
                    return NotFound();

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