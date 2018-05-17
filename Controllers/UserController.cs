using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api.Service;
using web_api.Dtos;
using web_api.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            this._service = service;
        }

        // Register Controller : Handle Register Request
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserDto newuser)
        {
            if(newuser == null)
                return BadRequest();

            if(await this._service.IsUserExist(newuser.Email))
            {
                ModelState.AddModelError("Email","Email exists");
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                Email = newuser.Email
            };

            var createdUser = await this._service.Register(userToCreate, newuser.Password);

            if (createdUser == null)
            {
                return BadRequest();
            }

            return StatusCode(201);

        }

        [HttpPost("login",Name = "Login")]
        public async Task<IActionResult> Login([FromBody]LoginDto userCredentials){
            // Validate request
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            // Check if user exist
            if(!(await this._service.IsUserExist(userCredentials.Email))){
                ModelState.AddModelError("User Errror","User is not exist");
                return BadRequest(ModelState);                
            }

            // Login
            var loginCallback = await this._service.Login(userCredentials.Email,userCredentials.Password);
            // fail to login
            if(loginCallback == null)
            {
                return Unauthorized();
            }
            
            // return StatusCode(200);

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("My Screct");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject= new ClaimsIdentity(
                    new Claim[]{
                        new Claim(ClaimTypes.NameIdentifier,loginCallback.ID.ToString()),
                        new Claim(ClaimTypes.Name, loginCallback.Email)
                    }
                ),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)   
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok( new {tokenString});
        }
    }
}