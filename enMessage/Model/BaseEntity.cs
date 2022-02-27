using System.ComponentModel.DataAnnotations;

namespace Model
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid ID { get; set; }
    }
}
