using System.ComponentModel.DataAnnotations;

namespace enMessage.Model
{
    public class Chat : BaseEntity
    {
        [Required]
        public string ChatName { get; set; }
        [Required]
        public DateTime LastInteraction { get; set; }
        [Required]
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
