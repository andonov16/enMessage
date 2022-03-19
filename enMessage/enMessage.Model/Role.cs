using System.ComponentModel.DataAnnotations;

namespace enMessage.Model
{
    public class Role:BaseEntity
    {
        [Required]
        public User Holder { get; set; }
        [Required]
        public Chat ChatRoom { get; set; }
        [Required]
        public string RoleInChat { get; set; }
    }
}
