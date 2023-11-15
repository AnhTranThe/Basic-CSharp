using Basic_CSharp.Models;
using Basic_CSharp.ViewModels;
using System.Data.SqlClient;

namespace Basic_CSharp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public string ConnectionString { get; set; }
        public OrderRepository(string _ConnectionString)
        {
            this.ConnectionString = _ConnectionString;
        }
        // METHOD IMPLEMENTATIONS
        public async Task<List<Order>> GET_ALL_Async()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GET_BY_ID_Async(Guid Id)
        {
            throw new NotImplementedException();

        }

        public async Task<ResponseMessage> ADD_Async(Order newOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> UPDATE_Async(Guid Id, Order ModifiedOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> DELETE_Async(Guid Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM ORDERS WHERE OrderId = @OrderId";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", Id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return new ResponseMessage
                            {
                                IsSuccess = true,
                                Message = "Order deleted successfully."
                            };
                        }
                        else
                        {
                            return new ResponseMessage
                            {
                                IsSuccess = false,
                                Message = "Order not found or failed to delete."
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ResponseMessage> ADD_ORDER_FROM_CART_ITEMS_Async(List<ProductInCartViewModel> productsInCart, decimal AmountOrder, Guid userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    Guid newOrderId = Guid.NewGuid();
                    // Start a new transaction
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Insert order information into the Orders table

                            string insertOrderQuery = "INSERT INTO Orders (OrderId, UserId,Amount, OrderDate) VALUES (@OrderId, @UserId, @Amount, @OrderDate)";
                            using (SqlCommand orderCommand = new SqlCommand(insertOrderQuery, connection, transaction))
                            {
                                orderCommand.Parameters.AddWithValue("@OrderId", newOrderId);
                                orderCommand.Parameters.AddWithValue("@UserId", userId);
                                orderCommand.Parameters.AddWithValue("@Amount", AmountOrder);
                                orderCommand.Parameters.AddWithValue("@OrderDate", DateTime.Now);

                                await orderCommand.ExecuteNonQueryAsync();
                            }

                            // Insert order item information into the OrderItems table for each product in the cart
                            string insertOrderItemQuery = "INSERT INTO ORDER_DETAILS (OrderId, ProductId, Quantity, CurrentPrice) VALUES (@OrderId, @ProductId, @Quantity, @CurrentPrice)";

                            foreach (ProductInCartViewModel product in productsInCart)
                            {
                                using (SqlCommand orderItemCommand = new SqlCommand(insertOrderItemQuery, connection, transaction))
                                {
                                    orderItemCommand.Parameters.AddWithValue("@OrderId", newOrderId); // Use the OrderId from the previous insert
                                    orderItemCommand.Parameters.AddWithValue("@ProductId", product.ProductId);
                                    orderItemCommand.Parameters.AddWithValue("@Quantity", product.Quantity); // Assuming you have a Quantity property in your Product class
                                    orderItemCommand.Parameters.AddWithValue("@CurrentPrice", product.Price);
                                    await orderItemCommand.ExecuteNonQueryAsync();
                                }
                            }

                            // Commit the transaction if everything is successful
                            transaction.Commit();

                            return new ResponseMessage
                            {
                                IsSuccess = true,
                                Message = "Order added successfully."
                            };
                        }
                        catch (Exception ex)
                        {
                            // An error occurred, rollback the transaction
                            transaction.Rollback();

                            return new ResponseMessage
                            {
                                IsSuccess = false,
                                Message = $"Failed to add order. {ex.Message}"
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    IsSuccess = false,
                    Message = $"An error occurred. {ex.Message}"
                };
            }
        }

    }
}
