using AutoMapper;
using biblioteka_api.DTOs;
using biblioteka_api.Hubs;
using biblioteka_api.Models;
using biblioteka_api.utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace biblioteka_api.Controllers
{
    public enum Status
    {
        Pending = 0,
        Accepted = 1,
        Issued=1,
        Returned = 2,
        Denied = 3
    }

    [Route("api/requests")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly BooksContext _dbContext;
        private readonly IMapper mapper;
        private readonly IHubContext<RefreshDataHub> _hubRefreshData;
        private readonly UserManager<IdentityUser> userManager;

        public RequestsController(BooksContext dbContext, IMapper mapper, IHubContext<RefreshDataHub> hubRefreshData,UserManager<IdentityUser> userManager)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
            this._hubRefreshData = hubRefreshData;
            this.userManager = userManager;
        }

        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDTO>>> GetRequests()
        {
            if (_dbContext.Requests == null)
            {
                return NotFound();
            }
            var requests = await _dbContext.Requests.ToListAsync();
            return mapper.Map<List<RequestDTO>>(requests);
        }*/

 

        [HttpGet("pending")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isLibrarian")]
        public async Task<ActionResult<IEnumerable<RequestDTO>>> GetRequestsPending()
        {
            if (_dbContext.Requests == null)
            {
                return NotFound();
            }

            var requests = await _dbContext.Requests
                .Include(x => x.Book)
                .Include(x => x.User)
                .Where(x => x.State == (int)Status.Pending)
                .ToListAsync();
            
            var result = mapper.Map<List<RequestDTO>>(requests);
            return result;
        }

        [HttpGet("issued")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isLibrarian")]
        public async Task<ActionResult<IEnumerable<RequestDTO>>> GetRequestsIssued()
        {
            if (_dbContext.Requests == null)
            {
                return NotFound();
            }

            var requests = await _dbContext.Requests
                .Include(x => x.Book)
                .Include(x => x.User)
                .Where(x => x.State == (int)Status.Issued)
                .ToListAsync();

            var result = mapper.Map<List<RequestDTO>>(requests);
            return result;
        }

        [HttpGet("user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isVisitor")]
        public async Task<ActionResult<IEnumerable<RequestDTO>>> GetRequestsUser()
        {
            if (_dbContext.Requests == null)
            {
                return NotFound();
                
            }
            string id = getUserID();

            var requests = await _dbContext.Requests
                .Include(x => x.Book)
                .Include(x=>x.User)
                .Where(x => x.UserId == id && x.State == (int)Status.Issued)
                .ToListAsync();

            var result = mapper.Map<List<RequestDTO>>(requests);
            return result;
        }

        [HttpGet("user/over")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isVisitor")]
        public async Task<ActionResult<IEnumerable<RequestDTO>>> GetRequestsUserOver()
        {
            if (_dbContext.Requests == null)
            {
                return NotFound();
            }
            var id = getUserID();
            var requests = await _dbContext.Requests
                .Include(x => x.Book)
                .Include(x => x.User)
                .Where(x => x.UserId == id && (x.State == (int)Status.Denied || x.State==(int)Status.Returned || x.State==(int)Status.Pending))
                .OrderBy(x => x.State)
                .ToListAsync();

            var result = mapper.Map<List<RequestDTO>>(requests);
            return result;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isVisitor")]
        public async Task<ActionResult> PostRequest([FromBody] RequestCreateDTO requestCreateDTO)
        {
            var request = mapper.Map<Request>(requestCreateDTO);
            request.State = 0;
            string id = getUserID();
            request.UserId = id;

            _dbContext.Requests.Add(request);
            await _dbContext.SaveChangesAsync();
            string groupId = "librarian";
            string message = "requestsAll";
            await _hubRefreshData.Clients.Group(groupId).SendAsync("RefreshDataMethod", message);

            return NoContent();
        }

        [HttpPut("accept/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isLibrarian")]
        public async Task<IActionResult> AcceptRequest(int id, [FromBody] string dateOfReturn, [FromHeader] string accept)
        {
            var result = await _dbContext.Requests.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == result.BookId);
                               
                 if (book != null) {
                    if (book.Available != 0)
                    {
                        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == result.UserId);
                        if (user.IssuedNumber < 5)
                        {
                            user.IssuedNumber++;
                            result.DateOfReturn = Convert.ToDateTime(dateOfReturn);
                            result.State = (int)Status.Accepted;
                            book.Available--;
                            await _dbContext.SaveChangesAsync();


                            string message = "booksAll";
                            await _hubRefreshData.Clients.All.SendAsync("RefreshDataMethod", message);

                            string groupId = "librarian";
                            message = "requestsAll";
                            await _hubRefreshData.Clients.Group(groupId).SendAsync("RefreshDataMethod", message);

                            string uid = result.UserId;
                            string umail = _dbContext.Users.FirstOrDefault(x => x.Id == uid).Email;
                            string clientId = RefreshDataHub.MyUsers.FirstOrDefault(x => x.Key == umail).Value;
                            message = "requestsAll";
                            await _hubRefreshData.Clients.Client(clientId).SendAsync("RefreshDataMethod", message);

                            return NoContent();
                        }
                        else
                        {
                            //Console.WriteLine(" cnt>5 ");
                            return BadRequest("User has 5 books already");
                        }
                    }
                    else
                    {
                        return BadRequest("book not found");
                    }

                }
                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("deny/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isLibrarian")]
        public async Task<IActionResult> DenyRequest(int id)
        {
            var result = await _dbContext.Requests.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                result.State = (int)Status.Denied;
                await _dbContext.SaveChangesAsync();

                string groupId = "librarian";
                string message = "requestsAll";
                await _hubRefreshData.Clients.Group(groupId).SendAsync("RefreshDataMethod", message);


                string uid = result.UserId;
                string umail = _dbContext.Users.FirstOrDefault(x => x.Id == uid).Email;
                string clientId = RefreshDataHub.MyUsers.FirstOrDefault(x => x.Key == umail).Value;
                message = "requestsAll";
                await _hubRefreshData.Clients.Client(clientId).SendAsync("RefreshDataMethod", message);

                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("return/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isLibrarian")]
        public async Task<IActionResult> ReturnRequest(int id)
        {
            var result = await _dbContext.Requests.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                result.State = (int)Status.Returned;
                // _dbContext.SaveChanges();
                //await _dbContext.SaveChangesAsync();
                var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == result.BookId);
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == result.UserId);
                if (book == null || user==null) { return NotFound(); }
                user.IssuedNumber--;

                book.Available++;
                await _dbContext.SaveChangesAsync();

                string message = "booksAll";
                await _hubRefreshData.Clients.All.SendAsync("RefreshDataMethod", message);

                string groupId = "librarian";
                message = "requestsAllReturned";
                await _hubRefreshData.Clients.Group(groupId).SendAsync("RefreshDataMethod", message);

                string uid = result.UserId;
                string umail = _dbContext.Users.FirstOrDefault(x => x.Id == uid).Email;
                string clientId = RefreshDataHub.MyUsers.FirstOrDefault(x => x.Key == umail).Value;
                message = "requestsAll";
                await _hubRefreshData.Clients.Client(clientId).SendAsync("RefreshDataMethod", message);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

         
        private string getUserID()
        {
            string email = HttpContext.Request.Headers["Authorization"];
            string token = email.Split(" ")[1];
            var jwt_token = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var mail = jwt_token.Claims.FirstOrDefault(c => c.Type == "email").Value;
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == mail);
            return user.Id;
        }

    }
}
