namespace paroquiaRussas.Models
{
    public class Event : BaseModel
    {
        public string EventDate { get; set; }

        public string EventTime { get; set; }

        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public string EventImage { get; set; }

        public string EventAddress { get; set; }
    }
}