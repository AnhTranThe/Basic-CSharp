using Basic_CSharp.Models;
using Basic_CSharp.ViewModels;

namespace Basic_CSharp.Repositories
{
    public interface ICartRepository : IGeneralRepository<Cart>
    {
        Task<ResponseMessage> ADD_CART_ITEMS_Async(CartDetail cartItem);
        Task<List<ProductInCartViewModel>> GET_PRODUCTS_IN_CART_Async(Guid Id);
        Task<ProductInCartViewModel> GET_PRODUCT_IN_CART_BY_ID_Async(Guid CartId, Guid ProductId);
        Task<int> CHECK_EXIST_PRODUCT_IN_CART_BY_ID_Async(Guid CartId, Guid ProductId);
        Task<ResponseMessage> UPDATE_PRODUCT_IN_CART_BY_ID_Async(CartDetail updateCartDetail);
        Task<ResponseMessage> DELETE_PRODUCTS_IN_CART_Async(Guid CartId);






    }
}
