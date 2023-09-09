namespace paroquiaRussas.Models
{
    public class News : BaseModel
    {
        public string? NewsTitle { get; set; }

        public string? Headline { get; set; }

        public string? NewsContent { get; set; }

        public string? NewsImage { get; set; }
    }
}
