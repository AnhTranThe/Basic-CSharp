using Basic_CSharp.Models;
using System.Data;
using System.Data.SqlClient;

namespace Basic_CSharp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }


        public async Task<List<Product>> GET_ALL_PRODUCTS_Async()
        {
            try
            {
                List<Product> productsLs = new List<Product>();
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.StatisticsEnabled = true;
                connection.FireInfoMessageEventOnUserErrors = true;
                connection.Open();
                //INSERT INTO PRODUCTS(ProductId, Product_Name, Price, Quantity, Category)
                string selectQuery = $"SELECT * FROM PRODUCTS";

                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection);
                DataTable dataTable = new DataTable();
                await Task.Run(() => adapter.Fill(dataTable));



                foreach (DataRow row in dataTable.Rows)
                {
                    Product product = new Product
                    {
                        ProductId = Guid.Parse(row["ProductId"].ToString()),
                        Product_Name = row["Product_Name"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        Category = row["Category"].ToString()

                    };

                    productsLs.Add(product);
                }
                connection.Close();
                return productsLs;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured: {ex.Message}");
            }

        }

        public async Task<Product> GET_PRODUCT_Async(string ProductId)
        {


            try
            {
                Product Chk_Product = new Product();
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.StatisticsEnabled = true;
                connection.FireInfoMessageEventOnUserErrors = true;
                connection.Open();
                //INSERT INTO PRODUCTS(ProductId, Product_Name, Price, Quantity, Category)
                string selectQuery = $"SELECT * FROM PRODUCTS WHERE ProductId = {Guid.Parse(ProductId)} ";
                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                if (dataTable.Rows.Count > 0)
                {

                    Chk_Product.ProductId = Guid.Parse(dataTable.Rows[0]["ProductId"].ToString());
                    Chk_Product.Product_Name = dataTable.Rows[0]["Product_Name"].ToString();
                    Chk_Product.Price = Convert.ToDecimal(dataTable.Rows[0]["Price"]);
                    Chk_Product.Quantity = Convert.ToInt32(dataTable.Rows[0]["Quantity"]);
                    Chk_Product.Category = dataTable.Rows[0]["Category"].ToString();

                }
                else
                {
                    Console.WriteLine("Product not found");
                }


                connection.Close();
                return Chk_Product;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured: {ex.Message}");
            }

        }

        public async Task<ResponseMessage> ADD_PRODUCT_Async(Product product)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.StatisticsEnabled = true;
                connection.FireInfoMessageEventOnUserErrors = true;
                connection.Open();
                //INSERT INTO PRODUCTS(ProductId, Product_Name, Price, Quantity, Category)
                string insertQuery = $"INSERT INTO PRODUCTS (ProductId, Product_Name, Price, Quantity, Category) VALUES(N'{product.ProductId}',N'{product.Product_Name}',{product.Price},{product.Quantity},N'{product.Category}')";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                int result = insertCommand.ExecuteNonQuery();
                connection.Close();
                if (result == 0)
                {

                    return new ResponseMessage
                    {
                        Message = "Add new product fail !"
                    };

                }

                else
                {
                    return new ResponseMessage
                    {
                        Message = "Add new product successfully !"
                    };

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured: {ex.Message}");
            }
        }

        public async Task<ResponseMessage> UPDATE_PRODUCT_Async()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> DELETE_PRODUCT_Async()
        {
            throw new NotImplementedException();
        }

    }
}
