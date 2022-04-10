using enMessage.DataAccess.Repositories;
using enMessage.Shared.Mappers;
using enMessage.Shared.Utilities;
using enMessage.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace enMessage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageRepository repo;
        private readonly UserRepository userRepo;
        private readonly ChatRepository chatRepo;
        private readonly RoleRepository roleRepo;



        public MessagesController(MessageRepository repo, UserRepository userRepo, ChatRepository chatRepo, RoleRepository roleRepo)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.chatRepo = chatRepo;
            this.roleRepo = roleRepo;
        }



        [HttpGet("{chatid}")]
        public async Task<ActionResult<ICollection<MessageViewModel>>> GetMessagesFromChat(Guid chatid)
        {
            var chat = await chatRepo.ReadFullAsync(chatid);
            var messages = chat.Messages.ToArray();


            var result = chat.Messages.Select(m => MessageMapper.GetMessageViewModel(m))
                .OrderByDescending(m => m.SentOn)
                .ToArray();
            return result;
        }


        [HttpPost("{userid}/{chatid}/{dataType}/{content}")]
        public async Task<ActionResult> PostMessage(Guid userid, Guid chatid, string dataType, string content)
        {
            var user = await userRepo.ReadAsync(userid);
            var chat = await chatRepo.ReadAsync(chatid);

            content = MessageUtil.EncryptMessage(JsonConvert.DeserializeObject<RSAParameters>(user.PublicKey), content);

            var newMessage = MessageMapper.GetMessage(chat, user, DateTime.Now, dataType, content);

            await repo.CreateAsync(newMessage);
            chat.Messages.Add(newMessage);
            chat.LastInteraction = DateTime.Now;    
            await chatRepo.UpdateAsync(chat);

            return Ok("Message created successfully!");
        }


        [HttpDelete("{userid}/{chatid}/{messageid}")]
        public async Task<IActionResult> DeleteMessage(Guid userid, Guid chatid, Guid messageid)
        {
            var userRole = (await roleRepo.FindAsync(r => r.Holder.ID == userid && r.ChatRoom.ID == chatid)).Single();
            if(userRole.RoleInChat != "admin")
            {
                return StatusCode(400, "You are not admin!");
            }

            var chat = await chatRepo.ReadFullAsync(chatid);
            var message = await repo.ReadAsync(messageid);

            chat.Messages.Remove(message);
            await repo.DeleteAsync(messageid);
            await chatRepo.UpdateAsync(chat);

            return Ok("Message deleted sucessfully!");
        }
    }
}
