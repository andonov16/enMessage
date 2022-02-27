using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class BaseEntity
    {
        [Key]
        public Guid ID { get; set; }
    }
}
