using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    //implement read full (reads the virtual props as well)
    public class ChatContext
    {
        private readonly Context _context;



        public ChatContext(Context context)
        {
            this._context = context;
        }



        public async Task Create(Chat item)
        {
            _context.Chats.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Chat> Read(int key)
        {
            return await _context.Chats.SingleAsync(c => c.ID == key);
        }
        public async Task<Chat> ReadFull(int key)
        {
            return await _context.Chats
                .Include(c => c.Picture)
                .Include(c => c.Messages)
                .Include(c => c.ChatUsers)
                .SingleAsync(c => c.ID == key);
        }


        public async Task<ICollection<Chat>> ReadAll()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task Update(Chat item)
        {
            Chat toUpdate = await Read(item.ID);

            toUpdate.Name = item.Name;
            toUpdate.LastInteraction = item.LastInteraction;

            await _context.SaveChangesAsync();
        }
        public async Task UpdateFull(Chat item)
        {
            Chat toUpdate = await ReadFull(item.ID);

            toUpdate.Name = item.Name;
            toUpdate.Picture = item.Picture;
            toUpdate.Messages = item.Messages;
            toUpdate.ChatUsers = item.ChatUsers;
            toUpdate.LastInteraction = item.LastInteraction;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int key)
        {
            _context.Chats.Remove(await Read(key));
            await _context.SaveChangesAsync();
        }
    }
}
