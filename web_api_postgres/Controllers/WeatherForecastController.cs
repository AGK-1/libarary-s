using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using web_api_postgres.Dto_s;
using web_api_postgres.Models;

namespace web_api_postgres.Controllers
{
    [ApiController]
    [Route("/")]
    //[Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<LibraryController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        [HttpGet("check-connection")]
        public async Task<IActionResult> CheckConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();
                return Ok("✅ Подключение успешно!");
            }
            catch (Exception ex)
            {
                return BadRequest($"❌ Ошибка подключения: {ex.Message}");
            }
        }
        public LibraryController(ILogger<LibraryController> logger, IConfiguration configuration, AppDbContext context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        int[] ziko = { 1, 2, 3, 4, 5, 6, 7, 8, 9, };
        int index = 0;
        [HttpGet("Getolino")]
        public IEnumerable<int> Get()
        {
            foreach (int num in GetNumbersWithDelay())
            {
                yield return num;
                Thread.Sleep(1000);
            }

        }


        [HttpPost("Add_Library")]
        public async Task<ActionResult<Library>> AddLibrary(CreateLibraryDto Dto_s)
        {
            var library = new Library
            {
                Name = Dto_s.name,

            };

            _context.libraries.Add(library);
            await _context.SaveChangesAsync();

            return library;
        }


        [HttpGet("get_all_library")]
        public async Task<ActionResult<Library>> GetAllLibrary()
        {
            var libraries = await _context.libraries.ToListAsync();
            if (libraries == null)
            {
                return NotFound();
            }
            return Ok(libraries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Library>> GetLibraryId(int id)
        {
            var library = await _context.libraries.FindAsync(id);
            if (library == null)
            {
                return NotFound();
            }
            return library;
        }

        [HttpPut("update_library:/{id}")]
        public async Task<ActionResult<Library>> UpdateLibrary(int id, CreateLibraryDto dto)
        {
            var library = await _context.libraries.FindAsync(id);
            if (library == null)
            {
                return NotFound();
            }
            library.Name = dto.name;    
            await _context.SaveChangesAsync();
            return library;
        }

        [HttpDelete("delete_library:/{id}")]
        public async Task<ActionResult<Library>> DeleteLibrary(int id)
        {
            var library = await _context.libraries.FindAsync(id);
            if (library == null)
            {
                return NotFound();
            }
            var deleted = _context.libraries.Remove(library);
            await _context.SaveChangesAsync();

            return Ok(library); 

        }

        [HttpPost("Add_new_book")]
        public async Task<ActionResult<Book>> AddBook(CreateBookDto Dto_s)
        {
            
           

            var book = new Book
            {
                Title = Dto_s.Title,
                Description = Dto_s.Description,
                Author = Dto_s.Author,
                LibraryId = Dto_s.LibraryId,
                
            };

            _context.books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        [HttpGet("get_all_books")]
        public async Task<ActionResult<Book>> getAllBooks()
        {
            var book = await _context.books.ToListAsync();
            return Ok(book);
        }

        [HttpGet("get_book:/{id}")]
        public async Task<ActionResult<Book>> GetWithId(int id)
        {
            var item = await _context.books.Include(b => b.Library).FirstOrDefaultAsync(b => b.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            
            return item;
        }
        private async Task<ActionResult<Library>> NotFoundResult()
        {
            return NotFound("Library not found");
        }


        static IEnumerable<int> GetNumbersWithDelay()
        {
            Console.WriteLine("Начинаем отдавать числа...");

            yield return 1;  // Отдали первое число, тут функция приостанавливается

            Console.WriteLine("Отдали 1, готовим 2...");
            yield return 2;  // Отдали второе число, снова пауза

            Console.WriteLine("Отдали 2, готовим 3...");
            yield return 3;  // Отдали третье число

            Console.WriteLine("Все числа отданы.");
        }
    }
}
