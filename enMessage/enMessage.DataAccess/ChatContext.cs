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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Foreign Keys and connections
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
            #endregion

            #region Indexes
            modelBuilder.Entity<User>()
               .HasIndex(u => u.Username)
               .IsUnique();

            modelBuilder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();
            #endregion
        }



        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }



        //TEMP
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-GIEF5RK;Database=enMessageDB;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
    }
}