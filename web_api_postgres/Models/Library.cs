namespace web_api_postgres.Models
{
    public class Library
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Book>? Books { get; set; }

    }
}
