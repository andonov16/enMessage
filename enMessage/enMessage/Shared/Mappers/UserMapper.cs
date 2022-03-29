using enMessage.Model;
using enMessage.Shared.Utilities;
using enMessage.Shared.ViewModels;
using System.Security.Cryptography;

namespace enMessage.Shared.Mappers
{
    public static class UserMapper
    {
        public static User GetUser(string username, string email, string password, string publicKey, string privateKey)
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
            var result = new UserViewModel()
            {
                ID = item.ID,
                Username =item.Username,
                PrivateKey = JsonUtil.ConvertFromJson<RSAParameters>(item.PrivateKey),
                Friends = item.Friends.Select(f => GetAsFriend(f)).ToList(),
            };

            if (includePublicKey)
                result.PublicKey = JsonUtil.ConvertFromJson<RSAParameters>(item.PublicKey);
            return result;
        }

        internal static UserViewModel GetAsFriend(User item)
        {
            return new UserViewModel()
            { 
                ID = item.ID,
                Username = item.Username,
                PrivateKey = JsonUtil.ConvertFromJson<RSAParameters>(item.PrivateKey),
            };
        }
    }
}
