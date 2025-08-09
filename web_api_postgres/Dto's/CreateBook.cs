namespace web_api_postgres.Dto_s
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int LibraryId { get; set; }
    }
}
