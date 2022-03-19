using Microsoft.EntityFrameworkCore;
using enMessage.Model;

namespace enMessage.DataAccess
{
    public class ChatContext:DbContext
    {
        public ChatContext() : base()
        {
            this.Database.EnsureCreated();
        }
        public ChatContext(DbContextOptions<ChatContext> options)
                                            : base(options)
        {
            this.Database.EnsureCreated();
        }

        internal object Entry<T>()
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Users)
                .WithMany(u => u.Chats);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.SentIn);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Roles)
                .WithOne(r => r.ChatRoom);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.SentIn)
                .WithMany(c => c.Messages);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users);
        }



        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }



        //TEMP
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-GIEF5RK;Database=enMessageDB;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
    }
}