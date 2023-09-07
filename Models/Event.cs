namespace paroquiaRussas.Models
{
    public class Event : BaseModel
    {
        public DateTime EventDate { get; set; }

        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public string EventImage { get; set; }

        public string EventAddress { get; set; }
    }
}