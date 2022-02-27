using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ChatRepository : BaseRepository<Chat>
    {
        public ChatRepository(Context context) : base(context)
        {
        }


        public override async Task<Chat> ReadFullAsync(Guid id)
        {
            return await context.Set<Chat>()
                    .Include(c => c.Messages)
                    .Include(c => c.ChatUsers)
                    .SingleAsync(r => r.ID == id);
        }

        public override async Task<ICollection<Chat>> ReadAllFullAsnyc()
        {
            return await context.Set<Chat>()
                    .Include(c => c.Messages)
                    .Include(c => c.ChatUsers)
                    .ToListAsync();
        }
    }
}
