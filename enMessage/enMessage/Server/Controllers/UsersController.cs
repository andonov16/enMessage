#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using enMessage.DataAccess;
using enMessage.DataAccess.Repositories;
using enMessage.Model;
using enMessage.Shared.ViewModels;
using enMessage.Shared.Mappers;

namespace enMessage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository repo;
        private readonly RequestRepository requestRepo;



        public UsersController(UserRepository repo, RequestRepository requestRepo)
        {
            this.repo = repo;
            this.requestRepo = requestRepo;
        }



        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(Guid id)
        {
            var user = await repo.ReadFullAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            //return user;
            return UserMapper.GetUserViewModel(user, true);
        }


        //разменя requestedfrom i requestedto
        [HttpPut("request/{myid}/{frname}")]
        public async Task<ActionResult<Request>> PutUser(Guid myid, string frname)
        {
            var me = await repo.ReadFullAsync(myid);


            if(me.Friends.Any(f => f.Username == frname))
            {
                return Ok("You already have " + frname + " as a friend!");
            }

            var findFr = await repo.FindFullAsync(u => u.Username == frname);
            var fr = findFr.Single();

            var newRequest = RequestMapper.GetRequest(me);

            await requestRepo.CreateAsync(newRequest);

            fr.Requests.Add(newRequest);
            await repo.UpdateAsync(fr);

            //return newRequest;


            return Ok("Request sent!");
        }



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await repo.DeleteAsync(id);

            return NoContent();
        }
    }
}
