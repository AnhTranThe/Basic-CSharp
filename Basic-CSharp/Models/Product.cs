using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_CSharp.Models
{
    [Table("PRODUCTS")]
    public class Product
    {

        public Guid ProductId { get; set; } = Guid.NewGuid();
        public string Product_Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public int Inventory { get; set; } = 0;
        public string Category { get; set; } = string.Empty;
    }
}
