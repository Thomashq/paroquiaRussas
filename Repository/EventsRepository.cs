using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;

namespace paroquiaRussas.Repository
{
    public class EventsRepository
    {
        private readonly AppDbContext _appDbContext;

        public EventsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Event> GetEvents()
        {
            try
            {
                return _appDbContext.Event.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public Event GetEventById(long id)
        {
            try
            {
                Event eventToGet = _appDbContext.Event.FirstOrDefault(x => x.Id == id);

                return eventToGet;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public List<Event> GetEventsByDate(string date) 
        {
            try
            {
                DateTime dateTime = DateTime.Parse(date);
                dateTime = dateTime.ToUniversalTime();

                return _appDbContext.Event.Where(x => x.EventDate == dateTime).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
        public Event UpdateEvent(Event eventUpdate)
        {
            try
            {
                Event eventToEdit = _appDbContext.Event.FirstOrDefault(x => x.Id == eventUpdate.Id);

                if (eventToEdit == null)
                    return null; // Retorna 404 caso o evento não seja encontrado

                if (eventUpdate.EventDate != null)
                {
                    DateTime dateTimeUtc = eventUpdate.EventDate.ToUniversalTime();
                    eventToEdit.EventDate = dateTimeUtc;
                }

                if (!string.IsNullOrEmpty(eventUpdate.EventDescription))
                    eventToEdit.EventDescription = eventUpdate.EventDescription;

                if (!string.IsNullOrEmpty(eventUpdate.EventAddress))
                    eventToEdit.EventAddress = eventUpdate.EventAddress;

                if (!string.IsNullOrEmpty(eventUpdate.EventImage))
                    eventToEdit.EventImage = eventUpdate.EventImage;

                if (!string.IsNullOrEmpty(eventUpdate.EventName))
                    eventToEdit.EventName = eventUpdate.EventName;

                eventToEdit.UpdateDate = DateOnly.FromDateTime(DateTime.Now);

                _appDbContext.Event.Update(eventToEdit);

                return eventToEdit;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
        
        public Event CreateNewEvent(Event eventToPost)
        {
            try
            {
                DateTime dateTimeUtc = eventToPost.EventDate.ToUniversalTime();
                eventToPost.EventDate = dateTimeUtc;

                _appDbContext.Add(eventToPost);

                return eventToPost;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public Event DeleteEventById(long id)
        {
            try
            {
                Event eventToDelete = _appDbContext.Event.FirstOrDefault(x => x.Id == id);

                if (eventToDelete == null)
                    return null;

                _appDbContext.Event.Remove(eventToDelete);

                return eventToDelete;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        } 
    }
}
