namespace Basic_CSharp.ViewModels
{

    public class ProductInOrderViewModel
    {

        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Product_Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; } = string.Empty;


    }
}
