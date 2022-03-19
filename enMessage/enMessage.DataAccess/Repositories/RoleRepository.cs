using enMessage.Model;

namespace enMessage.DataAccess.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(ChatContext context) : base(context)
        {
        }
    }
}
