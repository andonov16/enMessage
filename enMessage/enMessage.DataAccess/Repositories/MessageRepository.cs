using enMessage.Model;
using Microsoft.EntityFrameworkCore;

namespace enMessage.DataAccess.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(ChatContext context) : base(context)
        {
        }
    }
}
