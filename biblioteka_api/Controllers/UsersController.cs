using biblioteka_api.DTOs;
using biblioteka_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace biblioteka_api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IConfiguration configuration, BooksContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this._dbContext = dbContext;
        }

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly BooksContext _dbContext;

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationDTO>> Register([FromBody] UserLoginDTO userLoginDTO)
        {
            var user = new User { UserName = userLoginDTO.Email, Email = userLoginDTO.Email, Type = userLoginDTO.Type };
            var res = await userManager.CreateAsync(user, userLoginDTO.Password);
            if (res.Succeeded)
            {
                return await createToken(userLoginDTO);
            }
            else
            {
                return BadRequest(res.Errors);
            }
        }

        private async Task<AuthenticationDTO> createToken(UserLoginDTO userLogin)
        {         
            var claims = new List<Claim>()
            {
                new Claim("email", userLogin.Email),

             };

            var user = await userManager.FindByNameAsync(userLogin.Email);
            var userType = _dbContext.Users.FirstOrDefault(x=>x.Email==userLogin.Email);

            if (userType.Type == 0)
            {
                claims.Add(new Claim("role", "librarian"));
            }
            else
            {
                claims.Add(new Claim("role", "visitor"));
            }

            var userClaims = await userManager.GetClaimsAsync(user);           
            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, signingCredentials: credentials);

            return new AuthenticationDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationDTO>> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            var res = await signInManager.PasswordSignInAsync(userLoginDTO.Email, userLoginDTO.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (res.Succeeded)
            {           
                return await createToken(userLoginDTO);
            }
            else
            {
                return BadRequest("Incorrect");
            }
        }
    }
}
