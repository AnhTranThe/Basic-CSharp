using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_CSharp.Models
{
    [Table("ORDERS")]
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = new User();
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();


    }
}
