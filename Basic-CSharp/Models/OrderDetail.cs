using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_CSharp.Models
{
    [Table("ORDER_DETAILS")]
    public class OrderDetail
    {
        public Guid OrderId { get; set; }
        public virtual Order Cart { get; set; } = new Order();
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
        public decimal CurrentPrice { get; set; } = 0;

    }
}
