using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_CSharp.Models
{
    [Table("CART_DETAILS")]
    public class CartDetail
    {
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; } = new Cart();
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
