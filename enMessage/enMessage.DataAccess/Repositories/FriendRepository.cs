using enMessage.Model;

namespace enMessage.DataAccess.Repositories
{
    public class FriendRepository : BaseRepository<Friend>
    {
        public FriendRepository(ChatContext context) : base(context)
        {
        }
    }
}
