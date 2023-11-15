using Basic_CSharp.Models;
using Basic_CSharp.ViewModels;

namespace Basic_CSharp.Repositories
{
    public interface IOrderRepository : IGeneralRepository<Order>
    {
        // METHOD IMPLEMENTATIONS
        Task<ResponseMessage> ADD_ORDER_FROM_CART_ITEMS_Async(List<ProductInCartViewModel> productsInCart, decimal AmountOrder, Guid userId);


    }
}
