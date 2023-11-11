using Basic_CSharp.Models;
using Microsoft.Extensions.Configuration;

namespace Basic_CSharp.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CartRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = this._configuration.GetConnectionString("Default");
        }

        // METHOD IMPLEMENTATIONS
        public async Task<List<Cart>> GET_ALL_CARTS_Async()
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GET_CART_Async(Guid id)
        {
            throw new NotImplementedException();

        }

        public async Task<ResponseMessage> ADD_CART_Async(Cart cart)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> UPDATE_CART_Async(Cart cart)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> DELETE_CART_Async(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
