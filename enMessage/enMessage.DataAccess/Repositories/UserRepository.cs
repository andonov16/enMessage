using enMessage.Model;
using Microsoft.EntityFrameworkCore;

namespace enMessage.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ChatContext context) : base(context)
        {
        }


        public override async Task<User> ReadFullAsync(Guid id)
        {
            return await context.Users
                .Include(u => u.Requests)
                .Include(u => u.Chats)
                .SingleAsync(u => u.ID == id);
        }

        public override async Task<ICollection<User>> ReadAllFullAsync()
        {
            return await context.Users
                .Include(u => u.Requests)
                .Include(u => u.Chats)
                .ToListAsync();
        }

        public override async Task<ICollection<User>> FindFullAsync(Func<User, bool> check)
        {
            var allItems = await ReadAllFullAsync();
            return allItems.Where(check).ToList();
        }
    }
}
