using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using enMessage.DataAccess.Repositories;
using enMessage.Shared.ViewModels;
using enMessage.Shared.Mappers;
using enMessage.Model;

namespace enMessage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ChatRepository repo;
        private readonly UserRepository userRepo;
        private readonly RoleRepository roleRepo;


        public ChatsController(ChatRepository repo, UserRepository userRepo, RoleRepository roleRepo)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.roleRepo = roleRepo;   
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ChatViewModel>> GetChat(Guid id)
        {
            return ChatMapper.GetChatViewModel(await repo.ReadFullAsync(id));
        }

        [HttpPost("createchat/{myid}/{chatName}")]
        public async Task<IActionResult> PostChat(Guid myid, string chatName)
        {
            var me = await userRepo.ReadAsync(myid);
            var chat = ChatMapper.GetChat(chatName, DateTime.Now);

            await repo.CreateAsync(chat);

            return Ok("Chat created sucessfully!");
        }

        [HttpPut("addmembers/{myid}/{chatname}/{usernameslist}")]
        public async Task<IActionResult> AddMembersInChat(Guid myid, string chatname, string usernameslist)
        {
            var usernames = usernameslist.Split(",").ToList();
            var me = await userRepo.ReadAsync(myid);
            if(me == null)
            {
                return StatusCode(400, "Invalid user id!");
            }

            var chat = (await repo.FindFullAsync(c => c.ChatName == chatname)).First();
            var adminRole = RoleMapper.GetRole(me, chat, "admin");

            var members = await GetUsersByUsername(usernames);
            foreach (var member in members)
            {
                chat.Users.Add(member);
            }
            await repo.UpdateAsync(chat);

            members = await GetFullUsersByUsername(usernames);
            foreach (var member in members)
            {
                member.Chats.Add(chat);
                await userRepo.UpdateAsync(member);
            }

            await roleRepo.CreateAsync(adminRole);

            return Ok("Members added sucessfully!");
        }


        [HttpPut("leavechat/{myid}/{chatid}")]
        public async Task<IActionResult> LeaveChat(Guid myId, Guid chatId)
        {
            var me = await userRepo.ReadFullAsync(myId);
            var chat = await repo.ReadFullAsync(chatId);
            var roles = chat.Roles.Where(r => r.Holder.ID == me.ID);

            me.Chats.Remove(chat);
            chat.Users.Remove(me);

            foreach (var role in roles)
            {
                chat.Roles.Remove(role);
                await roleRepo.DeleteAsync(role.ID);
            }
            await repo.UpdateAsync(chat);
            await userRepo.UpdateAsync(me);

            return Ok("You have left the chat!");
        }

        private async Task<ICollection<User>> GetUsersByUsername(ICollection<string> usernames)
        {
            var result = new List<User>();

            foreach (var username in usernames)
            {
                var user = await userRepo.FindAsync(u => u.Username == username);
                result.Add(user.Single());
            }

            return result;
        }
        private async Task<ICollection<User>> GetFullUsersByUsername(ICollection<string> usernames)
        {
            var result = new List<User>();

            foreach (var username in usernames)
            {
                var user = await userRepo.FindFullAsync(u => u.Username == username);
                result.Add(user.Single());
            }

            return result;
        }
    }
}
