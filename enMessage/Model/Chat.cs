using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Chat:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        //URL to pucture
        public string Picture { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        [Required]
        public virtual ICollection<ChatUser> ChatUsers { get; set; }
        public DateTime? LastInteraction { get; set; }
    }
}
