using enMessage.Model;
using enMessage.Shared.Utilities;
using enMessage.Shared.ViewModels;
using Newtonsoft.Json;
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
                Friends = "[]",
                Requests = new List<Request>(),
                Chats = new List<Chat>()
            };
        }

        public static UserViewModel GetUserViewModel(User item, bool includePublicKey)
        {
            var result = new UserViewModel()
            {
                ID = item.ID,
                Username =item.Username,
                PrivateKey = item.PrivateKey,
                Friends = JsonConvert.DeserializeObject<List<User>>(item.Friends).Select(f => GetAsFriend(f)).ToList(),
                Chats = item.Chats.Select(c => ChatMapper.GetChatViewModel(c)).ToList(),
                Requests = item.Requests.Select(r => RequestMapper.GetRequestViewModel(r)).ToList()
            };

            if (includePublicKey)
            {
                result.PublicKey = item.PrivateKey;
            }

            return result;
        }

        internal static UserViewModel GetAsFriend(User item)
        {
            return new UserViewModel()
            { 
                ID = item.ID,
                Username = item.Username,
                PrivateKey = item.PrivateKey
            };
        }
    }
}
