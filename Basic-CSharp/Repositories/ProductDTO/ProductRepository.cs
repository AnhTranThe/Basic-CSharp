using Basic_CSharp.Models;
using Basic_CSharp.Utilities;
using System.Data.SqlClient;

namespace Basic_CSharp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public string ConnectionString { get; set; }
        public ProductRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public async Task<List<Product>> GET_ALL_Async()
        {
            List<Product> productLs = new List<Product>();
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            try
            {
                string selectQuery = $"SELECT * FROM PRODUCTS";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {

                    Product row_Product = new Product();
                    int index_ProductId = CommonUtils.GetIntFromDataReader(dataReader, "ProductId");
                    int index_Product_Name = CommonUtils.GetIntFromDataReader(dataReader, "Product_Name");
                    int index_Price = CommonUtils.GetIntFromDataReader(dataReader, "Price");
                    int index_Inventory = CommonUtils.GetIntFromDataReader(dataReader, "Inventory");
                    int index_Category = CommonUtils.GetIntFromDataReader(dataReader, "Category");

                    // Access columns by name or index
                    row_Product.ProductId = !dataReader.IsDBNull(index_ProductId) ? dataReader.GetGuid(index_ProductId) : Guid.Empty;
                    row_Product.Product_Name = !dataReader.IsDBNull(index_Product_Name) ? dataReader.GetString(index_Product_Name) : string.Empty;
                    row_Product.Price = !dataReader.IsDBNull(index_Price) ? dataReader.GetDecimal(index_Price) : 0;
                    row_Product.Inventory = !dataReader.IsDBNull(index_Inventory) ? dataReader.GetInt32(index_Inventory) : 0;
                    row_Product.Category = !dataReader.IsDBNull(index_Category) ? dataReader.GetString(index_Category) : string.Empty;

                    productLs.Add(row_Product);
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
            return productLs;

        }

        public async Task<Product> GET_BY_ID_Async(Guid Id)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            Product Chk_Product = new Product();
            try
            {


                string selectQuery = $"SELECT * FROM PRODUCTS WHERE ProductId = @productId ";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@productId", Id);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    int index_ProductId = CommonUtils.GetIntFromDataReader(dataReader, "ProductId");
                    int index_Product_Name = CommonUtils.GetIntFromDataReader(dataReader, "Product_Name");
                    int index_Price = CommonUtils.GetIntFromDataReader(dataReader, "Price");
                    int index_Inventory = CommonUtils.GetIntFromDataReader(dataReader, "Inventory");
                    int index_Category = CommonUtils.GetIntFromDataReader(dataReader, "Category");

                    // Access columns by name or index
                    Chk_Product.ProductId = !dataReader.IsDBNull(index_ProductId) ? dataReader.GetGuid(index_ProductId) : Guid.Empty;
                    Chk_Product.Product_Name = !dataReader.IsDBNull(index_Product_Name) ? dataReader.GetString(index_Product_Name) : string.Empty;
                    Chk_Product.Price = !dataReader.IsDBNull(index_Price) ? dataReader.GetDecimal(index_Price) : 0;
                    Chk_Product.Inventory = !dataReader.IsDBNull(index_Inventory) ? dataReader.GetInt32(index_Inventory) : 0;
                    Chk_Product.Category = !dataReader.IsDBNull(index_Category) ? dataReader.GetString(index_Category) : string.Empty;

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
            return Chk_Product;


        }

        public async Task<ResponseMessage> ADD_Async(Product newProduct)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {

                string insertQuery = "INSERT INTO PRODUCTS (ProductId, Product_Name, Price, Inventory, Category) " +
                    "VALUES (@productId, @productName, @price, @inventory, @category)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@productId", newProduct.ProductId);
                command.Parameters.AddWithValue("@productName", newProduct.Product_Name);
                command.Parameters.AddWithValue("@price", newProduct.Price);
                command.Parameters.AddWithValue("@inventory", newProduct.Inventory);
                command.Parameters.AddWithValue("@category", newProduct.Category);

                int result = await command.ExecuteNonQueryAsync();

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
                Console.WriteLine(ex.ToString());
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

        public async Task<ResponseMessage> UPDATE_Async(Guid Id, Product ModifiedProduct)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {

                string updateQuery = $"UPDATE PRODUCTS " +
                                  $"SET Product_Name = @productname," +
                                  $"    Price = @price, " +
                                  $"    Inventory = @inventory, " +
                                  $"    Category = @category " +
                                  $"WHERE ProductId = @id";


                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

                updateCommand.Parameters.AddWithValue("@id", Id);
                updateCommand.Parameters.AddWithValue("@productName", ModifiedProduct.Product_Name);
                updateCommand.Parameters.AddWithValue("@price", ModifiedProduct.Price);
                updateCommand.Parameters.AddWithValue("@inventory", ModifiedProduct.Inventory);
                updateCommand.Parameters.AddWithValue("@category", ModifiedProduct.Category);



                int result = await updateCommand.ExecuteNonQueryAsync();

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
                return new ResponseMessage
                {
                    IsSuccess = true,
                    Message = ex.Message

                };
            }

            finally
            {
                connection.Close();
            }
        }

        public async Task<ResponseMessage> DELETE_Async(Guid Id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {
                string deleteQuery = "DELETE FROM PRODUCTS WHERE ProductId = @productId";

                SqlCommand command = new SqlCommand(deleteQuery, connection);

                command.Parameters.AddWithValue("@productId", Id);

                int result = await command.ExecuteNonQueryAsync();

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
                Console.WriteLine(ex.ToString());
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

        public int CHECK_EXIST(Guid Id)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                connection.Open();
                string selectQuery = "SELECT COUNT(*) FROM PRODUCTS WHERE ProductId = @productId";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@productId", Id);

                object result = command.ExecuteScalarAsync();

                int cartCount = result != null ? Convert.ToInt32(result) : 0;
                connection.Close();
                return cartCount;







            }

        }


    }
}
