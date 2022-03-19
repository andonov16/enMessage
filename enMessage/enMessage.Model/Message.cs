using System.ComponentModel.DataAnnotations;

namespace enMessage.Model
{
    public class Message:BaseEntity
    {
        [Required]
        public Chat SentIn { get; set; }
        [Required]
        public User SentBy { get; set; }
        [Required]
        public DateTime SentOn { get; set; }
        [Required]
        public string DataType { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
