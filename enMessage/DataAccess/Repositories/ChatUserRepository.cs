using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ChatUserRepository
    {
        protected readonly Context context;



        public ChatUserRepository(Context context)
        {
            this.context = context;
        }



        public async Task CreateAsync(ChatUser item)
        {
            context.Set<ChatUser>().Add(item);
            await context.SaveChangesAsync();
        }

        public virtual async Task<ChatUser> ReadAsync(Guid id)
        {
            return await context.Set<ChatUser>().FindAsync(id);
        }

        public virtual async Task<ICollection<ChatUser>> ReadAllAsync()
        {
            var set = context.Set<ChatUser>().AsQueryable();
            return await set.ToListAsync();
        }

        public async Task UpdateAsync(ChatUser item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            ChatUser item = await ReadAsync(id);

            context.Set<ChatUser>().Remove(item);
            await context.SaveChangesAsync();
        }

        public virtual async Task<ICollection<ChatUser>> FindAsync(Func<ChatUser, bool> check)
        {
            var allItems = await ReadAllAsync();

            return allItems.Where(check).ToList();
        }
    }
}
