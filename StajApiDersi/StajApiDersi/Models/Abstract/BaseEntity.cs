using StajApiDersi.Enums;

namespace StajApiDersi.Models.Abstract
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public Status Status{ get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
