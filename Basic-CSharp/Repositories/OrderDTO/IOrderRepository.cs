using Basic_CSharp.Models;
using Basic_CSharp.ViewModels;

namespace Basic_CSharp.Repositories
{
    public interface IOrderRepository : IGeneralRepository<Order>
    {
        // METHOD IMPLEMENTATIONS
        Task<ResponseMessage> ADD_CART_TO_ORDER_DETAIL_Async(Order newOrder, List<ProductInCartViewModel> productsInCart);
        Task<List<ProductInOrderViewModel>> GET_PRODUCTS_IN_ORDERS_Async(Guid UserId);

    }
}
