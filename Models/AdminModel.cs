namespace paroquiaRussas.Models
{
    public class AdminModel
    {
        public AdminModel()
        {
            News = new News();
            Person = new Person();
            Event = new Event();
        }

        public List<News> NewsList { get; set; }

        public List<Event> EventList { get; set; }

        public List<Person> PersonList { get; set; }

        public News News { get; set; }

        public Event Event { get; set; }

        public Person Person { get; set; }
    }
}
