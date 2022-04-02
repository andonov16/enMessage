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
        public async Task<ActionResult<UserViewModel>> GetUserAsync(Guid id)
        {
            var user = await repo.ReadFullAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            //return user;
            return UserMapper.GetUserViewModel(user, true);
        }


        [HttpPost("request/{myid}/{frname}")]
        public async Task<IActionResult> SendFriendRequestAsync(Guid myid, string frname)
        {
            var me = await repo.ReadFullAsync(myid);


            if (me.Friends.Any(f => f.Username == frname))
            {
                return Ok("You already have " + frname + " as a friend!");
            }

            var findFr = await repo.FindFullAsync(u => u.Username == frname);
            var fr = findFr.Single();

            if (fr.Requests.Any(r => r.RequestedFromID == me.ID))
            {
                return StatusCode(400, "You already have sent request to " + fr.Username);
            }
            if(me.Requests.Any(r => r.RequestedFromID == fr.ID))
            {
                return StatusCode(400, "This request already exists!");
            }

            var newRequest = RequestMapper.GetRequest(me);

            await requestRepo.CreateAsync(newRequest);

            fr.Requests.Add(newRequest);
            await repo.UpdateAsync(fr);

            //return newRequest;


            return Ok("Request sent!");
        }



        [HttpPut("acceptfriend/{myid}/{requestid}")]
        public async Task<ActionResult<UserViewModel>> AcceptFriendRequestAsync(Guid myid, Guid requestid)
        {
            var me = await repo.ReadFullAsync(myid);
            var request = await requestRepo.ReadAsync(requestid);
            var newFriend = await repo.ReadFullAsync(request.RequestedFromID);

            me.Friends.Add(newFriend);
            newFriend.Friends.Add(me);
            me.Requests.Remove(request);

            //have to create chat!
            await repo.UpdateAsync(me);
            await repo.UpdateAsync(newFriend);
            await requestRepo.DeleteAsync(requestid);

            return Ok(newFriend.Username + "was added to your friends list!");
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await repo.DeleteAsync(id);

            return NoContent();
        }
    }
}
