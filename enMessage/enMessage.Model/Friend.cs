using System.ComponentModel.DataAnnotations;

namespace enMessage.Model
{
    public class Friend:BaseEntity
    {
        public Guid MyID { get; set; }
        public Guid FrID { get; set; }
    }
}
