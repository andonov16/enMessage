using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Model
{
    public class User
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Username { get; set; }
        public virtual byte[] ProfilePic { get; set; }
        [Required]
        //RSAPArameters -> byte[]
        public virtual byte[] RSAParams { get; set; }
        public virtual ICollection<User> Contacts { get; set; }
        public virtual ICollection<User> ContactRequests { get; set; }
        public virtual ICollection<ChatUser> ChatUsers { get; set; }
    }
}
