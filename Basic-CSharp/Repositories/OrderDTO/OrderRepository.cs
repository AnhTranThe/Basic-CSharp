using Basic_CSharp.Models;
using Microsoft.Extensions.Configuration;

namespace Basic_CSharp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        // METHOD IMPLEMENTATIONS
        public async Task<List<Order>> GET_ALL_ORDERS_Async()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GET_ORDER_Async(Guid id)
        {
            throw new NotImplementedException();

        }

        public async Task<ResponseMessage> ADD_ORDER_Async(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> UPDATE_ORDER_Async(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> DELETE_ORDER_Async(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
