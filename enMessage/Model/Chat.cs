using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Chat
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual byte[] Picture { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<ChatUser> ChatUsers { get; set; }
        public DateTime? LastInteraction { get; set; }
    }
}
