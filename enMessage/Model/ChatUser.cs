using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ChatUser
    {
        public string UserID { get; set; }
        public int ChatID { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
