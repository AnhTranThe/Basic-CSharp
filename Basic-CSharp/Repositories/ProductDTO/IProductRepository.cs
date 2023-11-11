using Basic_CSharp.Models;

namespace Basic_CSharp.Repositories
{
    public interface IProductRepository
    {
        // METHOD IMPLEMENTATIONS
        Task<List<Product>> GET_ALL_PRODUCTS_Async();

        Task<Product> GET_PRODUCT_Async(string ProductId);

        Task<ResponseMessage> ADD_PRODUCT_Async(Product product);

        Task<ResponseMessage> UPDATE_PRODUCT_Async();

        Task<ResponseMessage> DELETE_PRODUCT_Async();
    }
}
