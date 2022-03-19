using enMessage.Model;
using Microsoft.EntityFrameworkCore;

namespace enMessage.DataAccess.Repositories
{
    public class ChatRepository : BaseRepository<Chat>
    {
        public ChatRepository(ChatContext context) : base(context)
        {
        }



        public override async Task<Chat> ReadFullAsync(Guid id)
        {
            return await context.Chats
                .Include(c => c.Users)
                .Include(c => c.Messages)
                .Include(c => c.Roles)
                .SingleAsync(c => c.ID == id);
        }

        public override async Task<ICollection<Chat>> ReadAllFullAsync()
        {
            return await context.Chats
                .Include(c => c.Users)
                .Include(c => c.Messages)
                .Include(c => c.Roles)
                .ToListAsync();
        }

        public override async Task<ICollection<Chat>> FindFullAsync(Func<Chat, bool> check)
        {
            var allItems = await ReadAllFullAsync();
            return allItems.Where(check).ToList();
        }
    }
}
