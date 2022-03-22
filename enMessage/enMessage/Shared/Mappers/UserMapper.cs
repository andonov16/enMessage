using enMessage.Model;
using enMessage.Shared.Utilities;
using enMessage.Shared.ViewModels;
using System.Security.Cryptography;

namespace enMessage.Shared.Mappers
{
    public static class UserMapper
    {
        public static User GetUser(string username, string email, string password, byte[] publicKey, byte[] privateKey)
        {
            return new User()
            {
                Username = username,
                Email = email,
                Password = password,
                PublicKey = publicKey,
                PrivateKey = privateKey,
                Friends = new List<User>(),
                Requests = new List<User>(),
                Chats = new List<Chat>()
            };
        }

        public static UserViewModel GetUserViewModel(User item, bool includePublicKey)
        {
            //private UserViewModel LoadAsFriend
            return null;
        }
    }
}
