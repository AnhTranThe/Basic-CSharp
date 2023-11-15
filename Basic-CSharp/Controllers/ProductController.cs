using Basic_CSharp.Models;
using Basic_CSharp.Repositories;
using Basic_CSharp.Utilities;

namespace Basic_CSharp.Controllers
{
    public static class ProductController
    {

        static readonly ProductRepository productRepository = new ProductRepository(CommonUtils.GetConnectString());

        public static async Task INDEX()
        {
            Console.WriteLine($"{Environment.NewLine}Please select an option to proceed with Product");
            // DISPLAY OPTIONS
            Console.WriteLine("1. Add a product >");
            Console.WriteLine("2. View all products >");
            Console.WriteLine("3. View a product >");
            Console.WriteLine("4. Update a product >");
            Console.WriteLine("5. Delete a product >");
            Console.WriteLine("0. Exit >");

            // RECEIVE USER INPUT
            string? rdx_SELECTED_OPTION;
            int int_rdx_SELECTED_OPTION;

            do
            {
                rdx_SELECTED_OPTION = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION))
                {

                    Console.WriteLine("select Product option cannot be empty. Please try again.");
                }
                else
                {
                    if (!Int32.TryParse(rdx_SELECTED_OPTION, out int_rdx_SELECTED_OPTION))
                    {
                        Console.WriteLine("Must select number option from 0 to 5");

                    }

                    else
                    {
                        if (Array.IndexOf(ConstSystem.selectOptionsCRUD, int_rdx_SELECTED_OPTION) >= 0)
                        {
                            switch (int_rdx_SELECTED_OPTION)
                            {
                                case 0:
                                    await HomeController.Index();
                                    break;
                                case 1:
                                    await ADD_PRODUCT();
                                    break;
                                case 2:
                                    await VIEW_PRODUCTS_ALL();
                                    break;
                                case 3:
                                    await VIEW_PRODUCT();
                                    break;
                                case 4:
                                    await UPDATE_PRODUCT();
                                    break;
                                case 5:
                                    await DELETE_PRODUCT();
                                    break;
                                default:
                                    await INDEX();
                                    break;


                            }


                        }
                    }

                }


            }
            while (rdx_SELECTED_OPTION != null && String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION));

        }

        public static async Task ADD_PRODUCT()
        {


            string productname = string.Empty;
            decimal productPrice = 0;
            int productInventory = 0;
            string productCategory = string.Empty;
            do
            {

                Console.WriteLine("Enter product name >");
                productname = Console.ReadLine();
                // Check if the input is null or whitespace
                if (String.IsNullOrWhiteSpace(productname))
                {
                    Console.WriteLine("Product name cannot be empty. Please try again.");
                }



            } while (string.IsNullOrWhiteSpace(productname));


            do
            {


                Console.WriteLine("Enter product price >");

                // Check if the input is null or whitespace
                if (!decimal.TryParse(Console.ReadLine(), out productPrice) || productPrice <= 0)
                {
                    Console.WriteLine("Invalid product price. Please enter a valid positive numeric value");
                }

            } while (productPrice <= 0);


            do
            {


                Console.WriteLine("Enter product Inventory >");

                if (!Int32.TryParse(Console.ReadLine(), out productInventory) || productInventory <= 0)
                {
                    Console.WriteLine("Invalid product Inventory. Please enter a valid positive numeric value");
                }

            } while (productInventory <= 0);


            do
            {
                Console.WriteLine("Select Phone Category options below:");
                Console.WriteLine("1. Phone >");
                Console.WriteLine("2. Laptop >");
                Console.WriteLine("3. Accessories >");

                productCategory = Console.ReadLine();


                if (int.TryParse(productCategory, out int categoryOption))
                {
                    switch (categoryOption)
                    {
                        case 1:
                            productCategory = "Phone";
                            break;
                        case 2:
                            productCategory = "Laptop";
                            break;
                        case 3:
                            productCategory = "Accessories";
                            break;
                        default:
                            Console.WriteLine("Invalid category option. Please try again.");
                            break;
                    }

                }



                else
                {
                    Console.WriteLine("Product category cannot be empty. Please try again.");
                }


            } while (String.IsNullOrWhiteSpace(productCategory));


            // CREATE NEW PRODUCT INSTANCE
            Product newProduct = new Product
            {
                ProductId = Guid.NewGuid(),
                Product_Name = productname,
                Price = productPrice,
                Inventory = productInventory,
                Category = productCategory

            };

            try
            {
                ResponseMessage RESPONSE = await productRepository.ADD_Async(newProduct);

                if (RESPONSE != null && RESPONSE.IsSuccess)
                {
                    Console.WriteLine($"Product added successfully. Product ID: {newProduct.ProductId}");
                }
                else
                {
                    Console.WriteLine($"Failed to add new product.");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");

            }
            finally
            {
                await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("2"); // PRODUCT

            }

        }

        public static async Task VIEW_PRODUCTS_ALL()
        {
            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine("**** AVAILABLE PRODUCTS ****");

                List<Product> PRODUCTS = await productRepository.GET_ALL_Async();

                foreach (Product product in PRODUCTS)
                {
                    Console.WriteLine($"ProductId: {product.ProductId}, Name: {product.Product_Name}, Price: {product.Price}, Quantity: {product.Inventory}, Category: {product.Category}");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");

            }
            finally
            {
                await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("2"); // PRODUCT
            }
        }

        public static async Task VIEW_PRODUCT()
        {
            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine($"{Environment.NewLine}**** CHECK PRODUCT ****");
                Console.Write("Please enter Product Id:");
                Guid productId;
                do
                {

                    Console.WriteLine("Enter Product Id>");
                    // Check if the input is null or whitespace
                    string rdx_ProductId = Console.ReadLine();


                    if (!Guid.TryParse(rdx_ProductId, out productId))
                    {
                        Console.WriteLine("Invalid product id. Please enter a valid positive numeric value");
                    }
                    else if (string.IsNullOrWhiteSpace(rdx_ProductId))
                    {
                        Console.WriteLine("Product Id cannot be empty. Please try again.");
                    }



                } while (productId == Guid.Empty);

                Product product = await productRepository.GET_BY_ID_Async(productId);
                if (product == null)
                {
                    Console.WriteLine("Can't find existed Product");

                }
                else
                {
                    Console.WriteLine($"ProductId: {product.ProductId}, Name: {product.Product_Name}, Price: {product.Price}, Quantity: {product.Inventory}, Category: {product.Category}");

                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
            finally
            {
                await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("2"); // PRODUCT
            }
        }

        public static async Task UPDATE_PRODUCT()
        {
            try
            {
                // DISPLAY AVAILABLE PRODUCTS | IF ANY

                Console.WriteLine("**** AVAILABLE PRODUCTS ****");

                List<Product> PRODUCTS = await productRepository.GET_ALL_Async();

                foreach (Product product in PRODUCTS)
                {
                    Console.WriteLine($"ProductId: {product.ProductId}, Name: {product.Product_Name}, Price: {product.Price}, Quantity: {product.Inventory}, Category: {product.Category}");
                }


                Console.WriteLine($"{Environment.NewLine}Enter PRODUCT ID in order to update");

                Guid productId;
                do
                {

                    Console.WriteLine("Enter Product Id>");
                    // Check if the input is null or whitespace
                    string rdx_ProductId = Console.ReadLine();


                    if (!Guid.TryParse(rdx_ProductId, out productId))
                    {
                        Console.WriteLine("Invalid product id");
                    }
                    else if (string.IsNullOrWhiteSpace(rdx_ProductId))
                    {
                        Console.WriteLine("Product Id cannot be empty. Please try again.");
                    }



                } while (productId == Guid.Empty);

                Product existingProduct = await productRepository.GET_BY_ID_Async(productId);
                if (existingProduct == null)
                {
                    Console.WriteLine("Can't find existed Product");


                }
                else
                {


                    string productname = string.Empty;
                    decimal productPrice;
                    int productInventory;
                    string productCategory = string.Empty;

                    Console.WriteLine("Enter update product name >");
                    productname = Console.ReadLine();
                    // Check if the input is null or whitespace
                    if (String.IsNullOrWhiteSpace(productname))
                    {
                        productname = existingProduct.Product_Name;
                    }

                    do
                    {
                        Console.WriteLine("Enter update product price >");
                        string rdx_productPrice = Console.ReadLine();


                        if (String.IsNullOrWhiteSpace(rdx_productPrice))
                        {
                            // If no new price is entered, retain the existing product price
                            productPrice = existingProduct.Price;
                            break;
                        }


                        if (!decimal.TryParse(rdx_productPrice, out productPrice))
                        {

                            Console.WriteLine("Invalid price format. Please enter a valid numeric value for the price.");
                        }
                        else
                        {
                            // Valid input provided, exit the loop
                            break;
                        }
                    } while (true);


                    do
                    {
                        Console.WriteLine("Enter update product Inventory >");
                        string rdx_productInventory = Console.ReadLine();


                        if (String.IsNullOrWhiteSpace(rdx_productInventory))
                        {
                            // If no new price is entered, retain the existing product price
                            productInventory = existingProduct.Inventory;
                            break;
                        }


                        if (!Int32.TryParse(rdx_productInventory, out productInventory))
                        {

                            Console.WriteLine("Invalid Inventory format. Please enter a valid numeric value for the Inventory.");
                        }
                        else
                        {
                            // Valid input provided, exit the loop
                            break;
                        }
                    } while (true);


                    do
                    {
                        Console.WriteLine("Enter update product Category >");
                        string rdx_productCategory = Console.ReadLine();


                        if (String.IsNullOrWhiteSpace(rdx_productCategory))
                        {
                            // If no new price is entered, retain the existing product price
                            productCategory = existingProduct.Category;
                            break;
                        }

                        if (Array.IndexOf(ConstSystem.productCategoryArray, rdx_productCategory.Trim()) < 0)
                        {
                            Console.WriteLine("Invalid Category check in ('Phone', 'Laptop', 'Acccessories')");

                        }

                        else
                        {
                            // Valid input provided, exit the loop
                            // Valid input provided, exit the loop
                            productCategory = rdx_productCategory.Trim();
                            break;
                        }
                    } while (true);


                    Product updateProduct = new Product()
                    {
                        ProductId = existingProduct.ProductId,
                        Product_Name = productname,
                        Price = productPrice,
                        Inventory = productInventory,
                        Category = productCategory


                    };
                    ResponseMessage response = await productRepository.UPDATE_Async(existingProduct.ProductId, updateProduct);
                    if (response != null && response.IsSuccess)
                    {
                        Console.WriteLine($"Product updated successfully. Product ID: {existingProduct.ProductId}");


                    }
                    else
                    {
                        Console.WriteLine($"Product updated failed");

                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
            finally
            {

                await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("2"); // PRODUCT

            }
        }

        public static async Task DELETE_PRODUCT()
        {
            try
            {
                // DISPLAY AVAILABLE PRODUCTS | IF ANY
                Console.WriteLine("**** AVAILABLE PRODUCTS ****");

                List<Product> PRODUCTS = await productRepository.GET_ALL_Async();

                foreach (Product product in PRODUCTS)
                {
                    Console.WriteLine($"ProductId: {product.ProductId}, Name: {product.Product_Name}, Price: {product.Price}, Quantity: {product.Inventory}, Category: {product.Category}");
                }



                Console.WriteLine($"{Environment.NewLine}Enter PRODUCT ID in order to delete ");

                Guid productId;
                do
                {

                    Console.WriteLine("Enter Product Id>");
                    // Check if the input is null or whitespace
                    string rdx_ProductId = Console.ReadLine();


                    if (!Guid.TryParse(rdx_ProductId, out productId))
                    {
                        Console.WriteLine("Invalid product id. Please enter a valid positive numeric value");
                    }

                } while (productId == Guid.Empty);

                Product existingProduct = await productRepository.GET_BY_ID_Async(productId);
                if (existingProduct == null)
                {
                    Console.WriteLine("Can't find existed Product");


                }
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Please verify to delete User where Id = {existingProduct.ProductId} >");
                    Console.WriteLine("1. Yes >");
                    Console.WriteLine("2. No >");
                    Console.WriteLine("0. Exit >");

                    // RECEIVE USER INPUT
                    string? rdx_delete_options;
                    int int_rdx_delete_options;

                    do
                    {
                        rdx_delete_options = Console.ReadLine();

                        if (String.IsNullOrWhiteSpace(rdx_delete_options))
                        {

                            Console.WriteLine("select User option cannot be empty. Please try again.");
                        }
                        else if (!Int32.TryParse(rdx_delete_options, out int_rdx_delete_options))
                        {
                            Console.WriteLine("Must select number option from 0 to 2");
                        }

                        else
                        {
                            switch (int_rdx_delete_options)
                            {
                                case 0:
                                    await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("2"); // USER
                                    break;
                                case 1:
                                    ResponseMessage response = await productRepository.DELETE_Async(existingProduct.ProductId);
                                    if (response != null && response.IsSuccess)
                                    {
                                        Console.WriteLine("Delete Product Successfully !");

                                    }
                                    else
                                    {
                                        Console.WriteLine("Delete Product fail !");

                                    }
                                    await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("2"); // PRODUCT
                                    break;
                                default:
                                    Console.WriteLine("Invalid option. Please select 0 to 2");
                                    break;

                            }


                        }


                    }
                    while (rdx_delete_options != null && String.IsNullOrWhiteSpace(rdx_delete_options));


                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
        }


    }
}