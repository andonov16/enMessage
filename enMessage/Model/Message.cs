using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Message:BaseEntity
    {
        [Required]
        public string DataType { get; set; }
        [Required]
        public byte[] Data { get; set; }
        [Required]
        public User SentBy { get; set; }
        [Required]
        public DateTime SentOn { get; set; }
        public virtual ICollection<User> SeenBy { get; set; }
    }
}
