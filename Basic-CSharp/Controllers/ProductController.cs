using Basic_CSharp.Models;
using Basic_CSharp.Repositories;
using Basic_CSharp.Utilities;

namespace Basic_CSharp.Controllers
{
    public static class ProductController
    {

        static readonly ProductRepository productRepository = new ProductRepository(CommonUtils.GetConnectString());

        public static async Task ADD_PRODUCT()
        {
            // RECEIVE USER INPUT
            // TITLE
            Console.WriteLine("Enter product name >");
            string? productname = Console.ReadLine();

            // DESCRIPTION
            Console.WriteLine("Enter product price >");
            decimal productPrice = Convert.ToDecimal(Console.ReadLine());

            // PRICE
            Console.WriteLine("Enter product quantity >");
            int productQuantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Select Phone Category options below:");
            Console.WriteLine("1. Phone >");
            Console.WriteLine("2. Laptop >");
            Console.WriteLine("3. Accessories >");
            string? rdx_CATEGORY_SELECTED_OPTION = Console.ReadLine();
            string productCategory = "";
            if (!String.IsNullOrWhiteSpace(rdx_CATEGORY_SELECTED_OPTION))
            {

                switch (rdx_CATEGORY_SELECTED_OPTION)
                {
                    case "1":
                        productCategory = "Phone";
                        break;
                    case "2":
                        productCategory = "Laptop";
                        break;
                    case "3":
                        productCategory = "Accessories";
                        break;
                }
            }


            // CREATE NEW PRODUCT INSTANCE
            Product newProduct = new Product
            {
                ProductId = Guid.NewGuid(),
                Product_Name = productname,
                Price = productPrice,
                Quantity = productQuantity,
                Category = productCategory

            };

            // CALL SERVICES
            try
            {
                ResponseMessage RESPONSE = await productRepository.ADD_PRODUCT_Async(newProduct);

                Console.WriteLine(RESPONSE.Message);


            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
        }

        public static async Task VIEW_PRODUCTS_ALL()
        {
            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine("**** AVAILABLE PRODUCTS ****");

                List<Product> PRODUCTS = await productRepository.GET_ALL_PRODUCTS_Async();

                foreach (Product product in PRODUCTS)
                {
                    Console.WriteLine($"ProductId: {product.ProductId}, Name: {product.Product_Name}, Price: {product.Price}, Quantity: {product.Quantity}, Category: {product.Category}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
        }


        public static async Task VIEW_PRODUCT()
        {
            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine($"{Environment.NewLine}**** CHECK PRODUCT ****");
                Console.Write("Please enter product Id:");

                string? rdx_PRODUCTID_SELECTED_OPTION = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(rdx_PRODUCTID_SELECTED_OPTION))
                {
                    Product product = await productRepository.GET_PRODUCT_Async(rdx_PRODUCTID_SELECTED_OPTION);
                    Console.WriteLine($"ProductId: {product.ProductId}, Name: {product.Product_Name}, Price: {product.Price}, Quantity: {product.Quantity}, Category: {product.Category}");

                }





            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
        }

        public static async Task UPDATE_PRODUCT()
        {
            // DISPLAY AVAILABLE PRODUCTS | IF ANY
            await VIEW_PRODUCTS_ALL();
            // RECEIVE USER INPUT

            Console.WriteLine($"{Environment.NewLine}Enter PRODUCT ID in order to update a product e.g 1, 2, 3 etc....");

            int id = Int32.Parse(Console.ReadLine());

            // TITLE
            Console.WriteLine("Update product title >");
            string? productTitle = Console.ReadLine();

            // DESCRIPTION
            Console.WriteLine("Update product description >");
            string? productDescription = Console.ReadLine();

            // PRICE
            Console.WriteLine("Update product price >");
            string? productPrice = Console.ReadLine();

            // CREATE NEW PRODUCT INSTANCE
            Product updatedProduct = new Product
            {
                Id = id,
                Title = productTitle,
                Description = productDescription,
                Price = productPrice
            };

            // CALL SERVICES
            try
            {
                ResponseMessage RESPONSE = await productService.UPDATE_PRODUCT_Async(updatedProduct);

                Console.WriteLine(RESPONSE.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
        }




    }
}
