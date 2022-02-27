using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ChatUser
    {
        public Guid UserID { get; set; }
        //can be used as private nickname for the current chat. Equals to the username by deffault
        [Required]
        public string Nickname { get; set; }
        public Guid ChatID { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
