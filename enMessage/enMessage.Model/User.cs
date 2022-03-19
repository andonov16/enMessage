using System.ComponentModel.DataAnnotations;

namespace enMessage.Model
{
    public class User:BaseEntity
    {
        [Required]
        public string Username { get; set; }
        //Hashed
        [Required]
        public string Email { get; set; }
        //Hashed
        [Required]
        public string Password { get; set; }
        [Required]
        //RSAParameters
        public byte[] PublicKey { get; set; }
        [Required]
        //RSAParameters
        public byte[] PrivateKey { get; set; }
        public virtual ICollection<User> Friends { get; set;}
        public virtual ICollection<User> Requests { get; set;}
        public virtual ICollection<Chat> Chats { get; set;}
    }
}
