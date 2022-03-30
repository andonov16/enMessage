using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using enMessage.DataAccess;
using enMessage.DataAccess.Repositories;
using enMessage.Shared.Logs;
using enMessage.Shared.Utilities;
using enMessage.Shared.ViewModels;
using enMessage.Shared.Mappers;
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



        [HttpGet]
        public async Task<ActionResult<UserViewModel>> Login(string email, string password)
        {
            var data = await repo.FindAsync(u => u.Email == email && u.Password == password);
            if(data.Count != 1)
            {
                return StatusCode(400, "Invalid email or password!");
            }

            var result = UserMapper.GetUserViewModel(data.Single(), true);

            return Ok(result);
        }

        //email and password hashed by the client
        [HttpPost]
        public async Task<ActionResult> Register(UserLog registerInfo)
        {
            try
            {
                KeyGenerator kg = new KeyGenerator();
                User newUser = UserMapper.GetUser(registerInfo.Username, registerInfo.Email, registerInfo.Password,
                    JsonUtil.ConvertToJson(kg.GetPublicKey()),
                    JsonUtil.ConvertToJson(kg.GetPrivateKey()));

                await repo.CreateAsync(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Couldn`t create new user! Exception:" + ex.Message);
            }

            //return Ok("Account created sucessfully!");
            return StatusCode(200, "Account created!");
        }
    }
}