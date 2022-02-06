using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    public class ChatUserContext
    {
        private readonly Context _context;



        public ChatUserContext(Context context)
        {
            this._context = context;
        }



        public async Task Create(ChatUser item)
        {
            _context.ChatUsers.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ChatUser> Read(string userID, int chatID)
        {
            return await _context.ChatUsers.SingleAsync(cu => cu.ChatID == chatID && cu.UserID == userID);
        }

        public async Task<ICollection<ChatUser>> ReadAll()
        {
            return await _context.ChatUsers.ToListAsync();
        }

        public async Task Update(ChatUser item)
        {
            ChatUser toUpdate = await Read(item.UserID, item.ChatID);

            toUpdate.IsAdmin = item.IsAdmin;

            await _context.SaveChangesAsync();  
        }

        public async Task Delete(string userID, int chatID)
        {
            _context.ChatUsers.Remove(await Read(userID, chatID));
            await _context.SaveChangesAsync();
        }
    }
}
