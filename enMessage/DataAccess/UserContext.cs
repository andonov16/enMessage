using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    //implement read full (reads the virtual props as well)
    public class UserContext
    {
        private readonly Context _context;



        public UserContext(Context context)
        {
            this._context = context;
        }



        public async Task Create(User item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Read(string key)
        {
            return await _context.Users.SingleAsync(u => u.ID == key);
        }
        public async Task<User> ReadFull(string key)
        {
            return await _context.Users
                .Include(u => u.ProfilePic)
                .Include(u => u.RSAParams)
                .Include(u => u.Contacts)
                .Include(u => u.ContactRequests)
                .Include(u => u.ChatUsers)
                .SingleAsync(u => u.ID == key);
        }

        public async Task<ICollection<User>> ReadAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateFull(User item)
        {
            User toUpdate = await ReadFull(item.ID);

            toUpdate.Username = item.Username;
            toUpdate.ProfilePic = item.ProfilePic;
            toUpdate.RSAParams = item.RSAParams;
            toUpdate.Contacts = item.Contacts;
            toUpdate.ContactRequests = item.ContactRequests;
            toUpdate.ChatUsers = item.ChatUsers;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(string key)
        {
            _context.Users.Remove(await Read(key));
            await _context.SaveChangesAsync();
        }
    }
}
