#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using enMessage.DataAccess;
using enMessage.DataAccess.Repositories;
using enMessage.Model;
using enMessage.Shared.ViewModels;
using enMessage.Shared.Mappers;
using Newtonsoft.Json;

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
            var friends = JsonConvert.DeserializeObject<List<User>>(me.Friends);

            if (friends.Any(f => f.Username == frname))
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

            var myFriends = JsonConvert.DeserializeObject<ICollection<User>>(me.Friends);
            var newFriendFriends = JsonConvert.DeserializeObject<ICollection<User>>(newFriend.Friends);

            myFriends.Add(newFriend);
            newFriendFriends.Add(me);
            me.Requests.Remove(request);

            me.Friends = JsonConvert.SerializeObject(myFriends);
            newFriend.Friends = JsonConvert.SerializeObject(newFriendFriends);

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
