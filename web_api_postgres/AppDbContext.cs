using Microsoft.EntityFrameworkCore;
using web_api_postgres.Models;

namespace web_api_postgres
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Book> books { get; set; }
        public DbSet<Library> libraries {get; set;}

    }
}
