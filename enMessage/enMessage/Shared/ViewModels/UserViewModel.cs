using System.Security.Cryptography;

namespace enMessage.Shared.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        public string Username { get; set; }
        public string? PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public virtual ICollection<UserViewModel> Friends { get; set; }
        public virtual ICollection<RequestViewModel> Requests { get; set; }
        public virtual ICollection<ChatViewModel> Chats { get; set; }
    }
}
