﻿using Model;
using DataAccess.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(Context context) : base(context)
        {
        }



        public override async Task<Message> ReadFullAsync(Guid id)
        {
            return await context.Set<Message>()
                    .Include(c => c.SeenBy)
                    .SingleAsync(r => r.ID == id);
        }

        public override async Task<ICollection<Message>> ReadAllFullAsnyc()
        {
            return await context.Set<Message>()
                    .Include(c => c.SeenBy)
                    .ToListAsync();
        }

        //Make sure that the path is correct -> Chats\<ChatID>\<file>.<extention>
        public async Task SaveFileAsync(Message fileMessage, byte[] fileBytes)
        {
            await FileUtil.SaveFileAsync(fileMessage.Data, fileBytes);
        }
    }
}
