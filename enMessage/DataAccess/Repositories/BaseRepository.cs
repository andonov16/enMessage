using Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly Context context;



        public BaseRepository(Context context)
        {
            this.context = context;
        }



        public async Task CreateAsync(T item)
        {
            context.Set<T>().Add(item);
            await context.SaveChangesAsync();
        }

        public virtual async Task<T> ReadAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public virtual async Task<T> ReadFullAsync(Guid id)
        {
            return await ReadAsync(id);
        }

        public virtual async Task<ICollection<T>> ReadAllAsync()
        {
            var set = context.Set<T>().AsQueryable();
            return await set.ToListAsync();
        }
        public virtual async Task<ICollection<T>> ReadAllFullAsnyc()
        {
            return await ReadAllAsync();
        }

        public async Task UpdateAsync(T item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            T item = await ReadAsync(id);

            context.Set<T>().Remove(item);
            await context.SaveChangesAsync();
        }

        public virtual async Task<ICollection<T>> FindAsync(Func<T, bool> check)
        {
            var allItems = await this.ReadAllFullAsnyc();
                
            return allItems.Where(check).ToList();
        }
        public virtual async Task<ICollection<T>> FindFullAsync(Func<T, bool> check)
        {
            return await this.FindAsync(check);
        }
    }
}
