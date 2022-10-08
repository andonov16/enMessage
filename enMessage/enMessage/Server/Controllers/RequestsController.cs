#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using enMessage.DataAccess;
using enMessage.DataAccess.Repositories;
using enMessage.Model;
using enMessage.Shared.Mappers;
using enMessage.Shared.ViewModels;

namespace enMessage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly RequestRepository repo;



        public RequestsController(RequestRepository repo)
        {
            this.repo = repo;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<RequestViewModel>> GetRequestAsync(Guid id)
        {
            return RequestMapper.GetRequestViewModel(await repo.ReadAsync(id));
        }
        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestAsync(Guid id)
        {
            await repo.DeleteAsync(id);

            return NoContent();
        }
    }
}
