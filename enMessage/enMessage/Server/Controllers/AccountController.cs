using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using enMessage.DataAccess;
using enMessage.DataAccess.Repositories;
using enMessage.Shared.Logs;
using enMessage.Model;

namespace enMessage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserRepository repo;



        public AccountController(UserRepository repo)
        {
            this.repo = repo;   
        }



        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            var result = await repo.FindAsync(u => u.Email == email && u.Password == password);
            if(result.Count != 1)
            {
                return StatusCode(400, "Invalid email or password!");
            }

            return Ok(result.First());
        }

        [HttpPost("{username}/{email}/{password}")]
        public async Task<ActionResult> Register(string username, string email, string password)
        {
            User newUser = new User()
            {
                Username = username,
                Email = email,
                Password = password
            };

            //create public and private keys for the user
            try
            {
                await repo.CreateAsync(newUser);
            }
            catch (Exception)
            {
                return StatusCode(400, "Couldn`t create new user!");
            }

            return Ok("Account created sucessfully!");
        }
    }
}