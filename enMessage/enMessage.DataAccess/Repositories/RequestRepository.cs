using enMessage.Model;

namespace enMessage.DataAccess.Repositories
{
    public class RequestRepository : BaseRepository<Request>
    {
        public RequestRepository(ChatContext context) : base(context)
        {
        }
    }
}
