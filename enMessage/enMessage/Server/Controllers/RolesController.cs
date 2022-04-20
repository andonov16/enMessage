using enMessage.DataAccess.Repositories;
using enMessage.Shared.Mappers;
using enMessage.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace enMessage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleRepository repo;
        private readonly UserRepository userRepo;
        private readonly ChatRepository chatRepo;



        public RolesController(RoleRepository repo, UserRepository userRepo, ChatRepository chatRepo)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.chatRepo = chatRepo;
        }



        [HttpGet("{userid}/{chatid}")]
        public async Task<ActionResult<RoleViewModel?>> GetRole(Guid userid, Guid chatid)
        {
            var user = await userRepo.ReadAsync(userid);
            var chat = await chatRepo.ReadAsync(chatid);
            var role = (await repo.FindAsync(r => r.Holder == user && r.ChatRoom == chat)).SingleOrDefault();

            if(role == null)
            {
                return Ok("No roles found!");
            }

            return RoleMapper.GetRoleViewModel(role);
        }
    }
}
