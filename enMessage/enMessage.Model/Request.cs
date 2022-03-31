using System.ComponentModel.DataAnnotations;

namespace enMessage.Model
{
    public class Request:BaseEntity
    {
        [Required]
        public Guid RequestedFromID { get; set; }
        [Required]
        public string RequestedFromUsername { get; set; }
    }
}
