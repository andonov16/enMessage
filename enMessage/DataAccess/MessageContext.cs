using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    //implement read full (reads the virtual props as well)
    public class MessageContext
    {
        private readonly Context _context;



        public MessageContext(Context context)
        {
            this._context = context;
        }



        public async Task Create(Message Item)
        {
            _context.Messages.Add(Item);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> Read(int key)
        {
            return await _context.Messages.SingleAsync(u => u.ID == key);
        }
        public async Task<Message> ReadFull(int key)
        {
            return await _context.Messages
                .Include(m => m.SeenBy)
                .SingleAsync(u => u.ID == key);
        }

        public async Task<ICollection<Message>> ReadAll()
        {
            return await _context.Messages.ToListAsync();
        }

        public async void UpdateFull(Message item)
        {
            Message toUpdate = await ReadFull(item.ID);

            toUpdate.DataType = item.DataType;
            toUpdate.Data = item.Data;
            toUpdate.SentBy = item.SentBy;
            toUpdate.SeenBy = item.SeenBy;
            toUpdate.SentOn = item.SentOn;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int key)
        {
            _context.Messages.Remove(await Read(key));
            await _context.SaveChangesAsync();
        }
    }
}
