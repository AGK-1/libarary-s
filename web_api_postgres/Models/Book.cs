namespace web_api_postgres.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
