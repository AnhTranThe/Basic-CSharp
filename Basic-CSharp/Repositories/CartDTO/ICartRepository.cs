using Basic_CSharp.Models;

namespace Basic_CSharp.Repositories
{
    public interface ICartRepository
    {


        // METHOD IMPLEMENTATIONS
        Task<List<Models.Cart>> GET_ALL_CARTS_Async();

        Task<Models.Cart> GET_CART_Async(Guid id);

        Task<ResponseMessage> ADD_CART_Async(Models.Cart cart);

        Task<ResponseMessage> UPDATE_CART_Async(Models.Cart cart);

        Task<ResponseMessage> DELETE_CART_Async(Guid id);

    }
}
