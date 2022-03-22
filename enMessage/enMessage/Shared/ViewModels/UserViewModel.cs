using System.Security.Cryptography;

namespace enMessage.Shared.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        public string Username { get; set; }
        public RSAParameters? PublicKey { get; set; }
        public RSAParameters PrivateKey { get; set; }
        public virtual ICollection<UserViewModel> Friends { get; set; }
        public virtual ICollection<UserViewModel> Requests { get; set; }
        public virtual ICollection<ChatViewModel> Chats { get; set; }
    }
}
