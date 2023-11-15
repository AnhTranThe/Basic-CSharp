using Basic_CSharp.Models;
using Basic_CSharp.Utilities;
using Basic_CSharp.ViewModels;
using System.Data.SqlClient;

namespace Basic_CSharp.Repositories
{
    public class CartRepository : ICartRepository
    {
        public string ConnectionString { get; set; }
        public CartRepository(string _ConnectionString)
        {
            this.ConnectionString = _ConnectionString;
        }

        // METHOD IMPLEMENTATIONS
        public async Task<List<Cart>> GET_ALL_Async()
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GET_BY_ID_Async(Guid Id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            Cart Chk_Cart = new Cart();
            try
            {

                string selectQuery = "SELECT CartId, UserId FROM CARTS WHERE UserId = @userid";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@userid", Id);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    int index_CartId = dataReader.GetOrdinal("CartId");
                    int index_UserId = dataReader.GetOrdinal("UserId");

                    // Access columns by name or index
                    Chk_Cart.UserId = !dataReader.IsDBNull(index_UserId) ? dataReader.GetGuid(index_UserId) : Guid.Empty;
                    Chk_Cart.CartId = !dataReader.IsDBNull(index_CartId) ? dataReader.GetGuid(index_CartId) : Guid.Empty;

                }
                dataReader.Close();

            }

            catch (Exception ex)
            {
                throw new Exception($"An error occured: {ex.Message}");
            }
            finally
            {

                connection.Close();
            }
            return Chk_Cart;


        }

        public async Task<int> CHECK_EXIST(Guid Id)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT COUNT(*) FROM CARTS WHERE UserId = @userId";
                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@userid", Id);

                    object result = command.ExecuteScalar();
                    int cartCount = result != null ? Convert.ToInt32(result) : 0;
                    return cartCount;
                }

                catch (Exception ex)
                {
                    throw new Exception($"An error occured: {ex.Message}");

                }
                finally { connection.Close(); }

            }

        }


        public async Task<ResponseMessage> ADD_Async(Cart newCart)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {

                string insertQuery = "INSERT INTO CARTS (CartId, UserId) " +
                    "VALUES (@cartId, @userId)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@cartId", newCart.CartId);
                command.Parameters.AddWithValue("@userId", newCart.UserId);


                int result = command.ExecuteNonQuery();

                if (result == 0)
                {

                    return new ResponseMessage
                    {
                        IsSuccess = false

                    };

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured: {ex.Message}");
            }
            finally
            {
                connection.Close();

            }
            return new ResponseMessage
            {
                IsSuccess = true

            };
        }

        public async Task<ResponseMessage> UPDATE_Async(Guid Id, Cart MofifiedCart)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> DELETE_Async(Guid Id)
        {
            throw new NotImplementedException();
        }



        public async Task<ResponseMessage> DELETE_PRODUCTS_IN_CART_Async(Guid Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    string deleteQuery = "DELETE FROM CART_DETAILS WHERE ProductId = @Id";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new ResponseMessage
                            {
                                IsSuccess = true,
                                Message = "Products deleted from the cart successfully."
                            };
                        }
                        else
                        {
                            return new ResponseMessage
                            {
                                IsSuccess = false,
                                Message = "No products found in the cart with the specified Id."
                            };
                        }
                    }
                    await connection.CloseAsync();
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
        public async Task<ResponseMessage> ADD_CART_ITEMS_Async(CartDetail cartItem)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {

                string insertQuery = "INSERT INTO CART_DETAILS (CartId, ProductId, Quantity) " +
                    "VALUES (@cartId, @productId, @quantity)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@cartId", cartItem.CartId);
                command.Parameters.AddWithValue("@productId", cartItem.ProductId);
                command.Parameters.AddWithValue("@quantity", cartItem.Quantity);


                int result = command.ExecuteNonQuery();

                if (result == 0)
                {

                    return new ResponseMessage
                    {
                        IsSuccess = false

                    };

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured: {ex.Message}");
            }
            finally
            {
                connection.Close();

            }
            return new ResponseMessage
            {
                IsSuccess = true

            };
        }

        public async Task<List<ProductInCartViewModel>> GET_PRODUCTS_IN_CART_Async(Guid CartId)
        {

            List<ProductInCartViewModel> productsInCart = new List<ProductInCartViewModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();


                    string selectQuery = "SELECT cd.CartId, p.ProductId, p.Product_Name, p.Price, cd.Quantity, p.Category " +
                                         "FROM PRODUCTS p " +
                                         "INNER JOIN CART_DETAILS cd ON p.ProductId = cd.ProductId " +
                                         "WHERE cd.CartId = @CartId";

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@CartId", CartId);
                    SqlDataReader reader = command.ExecuteReader();

                    int idCounter = 1;
                    while (reader.Read())
                    {
                        int index_CartId = CommonUtils.GetIntFromDataReader(reader, "CartId");
                        int index_ProductId = CommonUtils.GetIntFromDataReader(reader, "ProductId");
                        int index_Product_Name = CommonUtils.GetIntFromDataReader(reader, "Product_Name");
                        int index_Price = CommonUtils.GetIntFromDataReader(reader, "Price");
                        int index_Quantity = CommonUtils.GetIntFromDataReader(reader, "Quantity");
                        int index_Category = CommonUtils.GetIntFromDataReader(reader, "Category");

                        ProductInCartViewModel productItem = new ProductInCartViewModel
                        {
                            Id = idCounter++,
                            CartId = !reader.IsDBNull(index_CartId) ? reader.GetGuid(index_CartId) : Guid.Empty,
                            ProductId = !reader.IsDBNull(index_ProductId) ? reader.GetGuid(index_ProductId) : Guid.Empty,
                            Product_Name = !reader.IsDBNull(index_Product_Name) ? reader.GetString(index_Product_Name) : string.Empty,
                            Price = !reader.IsDBNull(index_Price) ? reader.GetDecimal(index_Price) : 0,
                            Quantity = !reader.IsDBNull(index_Quantity) ? reader.GetInt32(index_Quantity) : 0,
                            Category = !reader.IsDBNull(index_Category) ? reader.GetString(index_Category) : string.Empty
                        };

                        productsInCart.Add(productItem);
                    }
                    connection.Close();

                }

                return productsInCart;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting products in cart: {ex.Message}");
                return new List<ProductInCartViewModel>();
            }
        }
        public async Task<ProductInCartViewModel> GET_PRODUCT_IN_CART_BY_ID_Async(Guid CartId, Guid ProductId)
        {

            ProductInCartViewModel productItemInCart = new ProductInCartViewModel();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();


                    string selectQuery = "SELECT * FROM CART_DETAILS WHERE CartId = @cartId AND ProductId = @productId";

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@cartId", CartId);
                    command.Parameters.AddWithValue("@productId", ProductId);


                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        int index_CartId = CommonUtils.GetIntFromDataReader(reader, "cartId");
                        int index_ProductId = CommonUtils.GetIntFromDataReader(reader, "productId");


                        productItemInCart = new ProductInCartViewModel
                        {

                            CartId = !reader.IsDBNull(index_CartId) ? reader.GetGuid(index_CartId) : Guid.Empty,
                            ProductId = !reader.IsDBNull(index_ProductId) ? reader.GetGuid(index_ProductId) : Guid.Empty,

                        };


                    }
                    connection.Close();

                }

                return productItemInCart;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting products in cart: {ex.Message}");
                return new ProductInCartViewModel();
            }

        }
        public async Task<int> CHECK_EXIST_PRODUCT_IN_CART_BY_ID_Async(Guid CartId, Guid ProductId)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT COUNT(*) FROM CART_DETAILS WHERE CartId = @cartid AND ProductId = @productId";
                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@cartid", CartId);
                    command.Parameters.AddWithValue("@productId", ProductId);

                    object result = command.ExecuteScalar();
                    int cartCount = result != null ? Convert.ToInt32(result) : 0;
                    return cartCount;
                }

                catch (Exception ex)
                {
                    throw new Exception($"An error occured: {ex.Message}");

                }
                finally { connection.Close(); }

            }

        }
        public async Task<ResponseMessage> UPDATE_PRODUCT_IN_CART_BY_ID_Async(CartDetail modifiedProductInCart)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {
                string updateQuery = "UPDATE CART_DETAILS " +
                                     "SET Quantity = @quantity" +
                                     " WHERE ProductId = @productId AND CartId = @cartId ";
                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@quantity", modifiedProductInCart.Quantity);
                updateCommand.Parameters.AddWithValue("@cartId", modifiedProductInCart.CartId);
                updateCommand.Parameters.AddWithValue("@productId", modifiedProductInCart.ProductId);

                int result = updateCommand.ExecuteNonQuery();

                if (result == 0)
                {

                    return new ResponseMessage
                    {
                        IsSuccess = false

                    };

                }

                else
                {

                    return new ResponseMessage
                    {
                        IsSuccess = true

                    };
                }


            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured: {ex.Message}");
            }

            finally
            {
                connection.Close();
            }

        }
    }
}
