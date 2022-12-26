using AutoMapper;
using biblioteka_api.DTOs;
using biblioteka_api.Hubs;
using biblioteka_api.Models;
using biblioteka_api.utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace biblioteka_api.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isLibrarian")]
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _dbContext;
        private readonly IMapper mapper;
        private readonly IHubContext<RefreshDataHub> _hubRefreshData;

        public BooksController(BooksContext dbContext, IMapper mapper, IHubContext<RefreshDataHub> hubRefreshData) 
        {
            this._dbContext= dbContext;
            this.mapper = mapper;
            this._hubRefreshData = hubRefreshData;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks([FromQuery] PaginationDTO paginationDTO)
        {
            if (_dbContext.Books == null)
            {
                return NotFound();
            }

            var queryable = _dbContext.Books.AsQueryable();
            await HttpContext.paginationNumber(queryable);
            var books = await queryable.OrderBy(x => x.Author).Paginate(paginationDTO).ToListAsync();

            return mapper.Map<List<BookDTO>>(books);
        }


        [HttpPost]
        public async Task<ActionResult> PostBook([FromBody] BookCreateDTO bookCreateDTO)
        {
            var book = mapper.Map<Book>(bookCreateDTO);
            if (BookExists(book.Title))
            {
                return BadRequest("book already exists");
            }
            _dbContext.Books.Add(book);

            await _dbContext.SaveChangesAsync();

            string message = "booksAll";
            await _hubRefreshData.Clients.All.SendAsync("RefreshDataMethod", message);

            return NoContent();
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> AddMoreBooks(int id, [FromBody] string num)
        {
            var result = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {               
                int val = Convert.ToInt32(num);
                result.Available+=val;
                await _dbContext.SaveChangesAsync();

                string message = "booksAll";
                await _hubRefreshData.Clients.All.SendAsync("RefreshDataMethod", message);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        private bool BookExists(string name)
        {
            return (_dbContext.Books?.Any(e => e.Title == name)).GetValueOrDefault();
        }

    }
}
