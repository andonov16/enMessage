using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using enMessage.DataAccess;
using enMessage.DataAccess.Repositories;
using enMessage.Shared.Logs;
using enMessage.Shared.Utilities;
using enMessage.Shared.ViewModels;
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
        public async Task<ActionResult<UserViewModel>> Login(string email, string password)
        {
            var result = await repo.FindAsync(u => u.Email == email && u.Password == password);
            if(result.Count != 1)
            {
                return StatusCode(400, "Invalid email or password!");
            }

            return Ok(result.First());
        }

        //email and password hashed by the client
        [HttpPost("{username}/{email}/{password}")]
        public async Task<ActionResult> Register(string username, string email, string password)
        {
            User newUser = new User()
            {
                Username = username,
                Email = password,
                Password = password
            };

            KeyGenerator kg = new KeyGenerator();
            newUser.PublicKey = BytesUtil.ConvertToBytes(kg.GetPublicKey());
            newUser.PrivateKey = BytesUtil.ConvertToBytes(kg.GetPrivateKey());

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