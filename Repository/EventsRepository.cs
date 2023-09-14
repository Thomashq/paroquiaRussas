using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Resources;
using System;

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
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC18, ex);
            }
        }

        public List<Event> GetEventsByDateAndName(string eventName, DateOnly eventDate)
        {
            try
            {
                List<Event> Events = new List<Event>();
                Events = _appDbContext.Event.Where(x => x.EventName.ToLower().Contains(eventName.ToLower()) && x.EventDate == eventDate).ToList();

                return Events;
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC18, ex);
            }
        }

        public Event GetEventById(long id)
        {
            try
            {
                Event eventToGet = _appDbContext.Event.FirstOrDefault(x => x.Id == id);

                return eventToGet;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Exceptions.EXC11, id), ex);
            }
        }

        public List<Event> GetEventsByDate(string date)
        {
            try
            {
                DateOnly dateOnly = DateOnly.Parse(date);

                return _appDbContext.Event.Where(x => x.EventDate == dateOnly).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Exceptions.EXC19, date), ex);
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
                    eventToEdit.EventDate = eventUpdate.EventDate;
                
                if(eventUpdate.EventTime != null)
                    eventToEdit.EventTime = eventUpdate.EventTime;

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
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC05, ex);
            }
        }

        public Event CreateNewEvent(Event eventToPost)
        {
            try
            {
                _appDbContext.Add(eventToPost);

                return eventToPost;
            }
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC04, ex);
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
            catch (Exception ex)
            {
                throw new Exception(Exceptions.EXC06, ex);
            }
        }
    }
}
