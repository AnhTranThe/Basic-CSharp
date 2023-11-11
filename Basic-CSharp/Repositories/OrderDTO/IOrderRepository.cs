using Basic_CSharp.Models;

namespace Basic_CSharp.Repositories
{
    public interface IOrderRepository
    {
        // METHOD IMPLEMENTATIONS
        Task<List<Order>> GET_ALL_ORDERS_Async();

        Task<Order> GET_ORDER_Async(Guid id);

        Task<ResponseMessage> ADD_ORDER_Async(Order order);

        Task<ResponseMessage> UPDATE_ORDER_Async(Order order);

        Task<ResponseMessage> DELETE_ORDER_Async(Guid id);

    }
}
