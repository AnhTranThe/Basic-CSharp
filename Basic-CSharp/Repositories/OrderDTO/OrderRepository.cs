using Basic_CSharp.Models;
using Basic_CSharp.Utilities;
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
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            Order Chk_Order = new Order();
            try
            {

                string selectQuery = "SELECT OrderId, UserId, Amount, OrderDate FROM ORDERS WHERE UserId = @userid";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@userid", Id);
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    int index_OrderId = dataReader.GetOrdinal("OrderId");
                    int index_UserId = dataReader.GetOrdinal("UserId");
                    int index_Amount = dataReader.GetOrdinal("Amount");
                    int index_OrderDate = dataReader.GetOrdinal("OrderDate");

                    // Access columns by name or index
                    Chk_Order.OrderId = !dataReader.IsDBNull(index_OrderId) ? dataReader.GetGuid(index_OrderId) : Guid.Empty;
                    Chk_Order.UserId = !dataReader.IsDBNull(index_UserId) ? dataReader.GetGuid(index_UserId) : Guid.Empty;
                    Chk_Order.Amount = !dataReader.IsDBNull(index_Amount) ? dataReader.GetDecimal(index_Amount) : 0;
                    Chk_Order.OrderDate = !dataReader.IsDBNull(index_OrderDate) ? dataReader.GetDateTime(index_Amount) : DateTime.MinValue;

                }
                dataReader.Close();

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {

                connection.Close();
            }
            return Chk_Order;


        }

        public async Task<ResponseMessage> ADD_Async(Order newOrder)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();


                    try
                    {
                        // Insert order information into the Orders table

                        string insertOrderQuery = "INSERT INTO Orders (OrderId, UserId,Amount, OrderDate) VALUES (@OrderId, @UserId, @Amount, @OrderDate)";
                        using (SqlCommand orderCommand = new SqlCommand(insertOrderQuery, connection))
                        {
                            orderCommand.Parameters.AddWithValue("@OrderId", newOrder.OrderId);
                            orderCommand.Parameters.AddWithValue("@UserId", newOrder.UserId);
                            orderCommand.Parameters.AddWithValue("@Amount", newOrder.Amount);
                            orderCommand.Parameters.AddWithValue("@OrderDate", newOrder.OrderDate);

                            await orderCommand.ExecuteNonQueryAsync();
                        }


                        return new ResponseMessage
                        {
                            IsSuccess = true,
                            Message = "Order added successfully."
                        };
                    }
                    catch (Exception ex)
                    {

                        return new ResponseMessage
                        {
                            IsSuccess = false,
                            Message = $"Failed to add order. {ex.Message}"
                        };

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

                        int rowsAffected = await command.ExecuteNonQueryAsync();

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


        public int CHECK_EXIST(Guid Id)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                connection.Open();
                string selectQuery = "SELECT COUNT(*) FROM ORDERS WHERE UserId = @userId";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@userid", Id);

                object result = command.ExecuteScalar();
                int cartCount = result != null ? Convert.ToInt32(result) : 0;
                connection.Close();

                return cartCount;


            }

        }


        public async Task<ResponseMessage> ADD_CART_TO_ORDER_DETAIL_Async(Order newOrder, List<ProductInCartViewModel> productsInCart)
        {
            try
            {
                ResponseMessage responseAddOrder = await ADD_Async(newOrder);

                if (responseAddOrder != null && responseAddOrder.IsSuccess)
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        await connection.OpenAsync();
                        SqlTransaction transaction = connection.BeginTransaction();

                        try
                        {
                            // Insert order item information into the OrderItems table for each product in the cart
                            string insertOrderItemQuery = "INSERT INTO ORDER_DETAILS (OrderId, ProductId, Quantity, CurrentPrice) VALUES (@OrderId, @ProductId, @Quantity, @CurrentPrice)";

                            foreach (ProductInCartViewModel product in productsInCart)
                            {
                                using (SqlCommand orderItemCommand = new SqlCommand(insertOrderItemQuery, connection, transaction))
                                {
                                    orderItemCommand.Parameters.AddWithValue("@OrderId", newOrder.OrderId);
                                    orderItemCommand.Parameters.AddWithValue("@ProductId", product.ProductId);
                                    orderItemCommand.Parameters.AddWithValue("@Quantity", product.Quantity);
                                    orderItemCommand.Parameters.AddWithValue("@CurrentPrice", product.Price);
                                    await orderItemCommand.ExecuteNonQueryAsync();
                                }
                            }

                            // Commit the transaction if everything is successful
                            transaction.Commit();

                            return new ResponseMessage
                            {
                                IsSuccess = true,
                                Message = "Order and order items added successfully."
                            };
                        }
                        catch (Exception ex)
                        {
                            // An error occurred, rollback the transaction
                            transaction.Rollback();

                            return new ResponseMessage
                            {
                                IsSuccess = false,
                                Message = $"Failed to add order and order items. {ex.Message}"
                            };
                        }
                    }
                }
                else
                {
                    return new ResponseMessage
                    {
                        IsSuccess = false,
                        Message = "Failed to add order. Check the response message for details."
                    };
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
        public async Task<List<ProductInOrderViewModel>> GET_PRODUCTS_IN_ORDERS_Async(Guid userId)
        {
            List<ProductInOrderViewModel> productInOrderLs = new List<ProductInOrderViewModel>();
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    // Retrieve user orders
                    string selectUserOrdersQuery = "SELECT OrderId, OrderDate, Amount, UserId  FROM Orders WHERE UserId = @UserId";
                    using (SqlCommand userOrdersCommand = new SqlCommand(selectUserOrdersQuery, connection))
                    {
                        userOrdersCommand.Parameters.AddWithValue("@UserId", userId);

                        SqlDataReader userOrdersReader = await userOrdersCommand.ExecuteReaderAsync();

                        List<Order> OrderLs = new List<Order>();


                        while (userOrdersReader.Read())
                        {
                            int index_OrderId = CommonUtils.GetIntFromDataReader(userOrdersReader, "OrderId");
                            int index_OrderDate = CommonUtils.GetIntFromDataReader(userOrdersReader, "OrderDate");
                            int index_Amount = CommonUtils.GetIntFromDataReader(userOrdersReader, "Amount");
                            int index_UserId = CommonUtils.GetIntFromDataReader(userOrdersReader, "UserId");


                            Order OrderItem = new Order
                            {

                                OrderId = !userOrdersReader.IsDBNull(index_OrderId) ? userOrdersReader.GetGuid(index_OrderId) : Guid.Empty,
                                OrderDate = !userOrdersReader.IsDBNull(index_OrderDate) ? userOrdersReader.GetDateTime(index_OrderDate) : DateTime.MinValue,
                                Amount = !userOrdersReader.IsDBNull(index_Amount) ? userOrdersReader.GetDecimal(index_Amount) : 0,
                                UserId = !userOrdersReader.IsDBNull(index_UserId) ? userOrdersReader.GetGuid(index_UserId) : Guid.Empty,

                            };

                            OrderLs.Add(OrderItem);

                        }


                        foreach (Order order in OrderLs)
                        {
                            // Retrieve products in each order
                            string selectProductsInOrderQuery = "SELECT p.ProductId, p.Product_Name, cd.CurrentPrice, cd.Quantity, cd.OrderId , p.Category " +
                                                                "FROM PRODUCTS p " +
                                                                "INNER JOIN ORDER_DETAILS cd ON p.ProductId = cd.ProductId " +
                                                                "WHERE cd.OrderId = @OrderId";



                            using (SqlCommand productsInOrderCommand = new SqlCommand(selectProductsInOrderQuery, connection))
                            {
                                productsInOrderCommand.Parameters.AddWithValue("@OrderId", order.OrderId);

                                SqlDataReader productsInOrderReader = await productsInOrderCommand.ExecuteReaderAsync();


                                // Display products in the order
                                while (productsInOrderReader.Read())
                                {
                                    int index_ProductId = CommonUtils.GetIntFromDataReader(productsInOrderReader, "ProductId");
                                    int index_Product_Name = CommonUtils.GetIntFromDataReader(productsInOrderReader, "Product_Name");
                                    int index_CurrentPrice = CommonUtils.GetIntFromDataReader(productsInOrderReader, "CurrentPrice");
                                    int index_Quantity = CommonUtils.GetIntFromDataReader(productsInOrderReader, "Quantity");
                                    int index_OrderId = CommonUtils.GetIntFromDataReader(productsInOrderReader, "OrderId");

                                    int index_Category = CommonUtils.GetIntFromDataReader(productsInOrderReader, "Category");


                                    ProductInOrderViewModel productInOrderItem = new ProductInOrderViewModel
                                    {
                                        ProductId = !productsInOrderReader.IsDBNull(index_ProductId) ? productsInOrderReader.GetGuid(index_ProductId) : Guid.Empty,
                                        OrderId = !productsInOrderReader.IsDBNull(index_OrderId) ? productsInOrderReader.GetGuid(index_OrderId) : Guid.Empty,
                                        Product_Name = !productsInOrderReader.IsDBNull(index_Product_Name) ? productsInOrderReader.GetString(index_Product_Name) : string.Empty,
                                        Price = !productsInOrderReader.IsDBNull(index_CurrentPrice) ? productsInOrderReader.GetDecimal(index_CurrentPrice) : 0,
                                        Quantity = !productsInOrderReader.IsDBNull(index_Quantity) ? productsInOrderReader.GetInt32(index_Quantity) : 0,
                                        Category = !productsInOrderReader.IsDBNull(index_Category) ? productsInOrderReader.GetString(index_Category) : string.Empty
                                    };

                                    productInOrderLs.Add(productInOrderItem);

                                }

                            }

                        }

                    }
                    await connection.CloseAsync();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return productInOrderLs;


        }
    }
}



