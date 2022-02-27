using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class User:BaseEntity
    {
        //Hashed email
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        //URL
        public string ProfilePic { get; set; }
        [Required]
        //RSAPArameters -> byte[]
        public byte[] RSAParams { get; set; }
        public virtual ICollection<User> Contacts { get; set; }
        public virtual ICollection<User> ContactRequests { get; set; }
        public virtual ICollection<ChatUser> ChatUsers { get; set; }
    }
}
