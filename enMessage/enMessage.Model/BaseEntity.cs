using System.ComponentModel.DataAnnotations;

namespace enMessage.Model
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid ID { get; set; }
    }
}
