using Basic_CSharp.Models;
using Basic_CSharp.Repositories;
using Basic_CSharp.Utilities;
using Basic_CSharp.ViewModels;
using System.Data;

namespace Basic_CSharp.Controllers
{
    public static class CartController
    {
        static readonly CartRepository cartRepository = new CartRepository(CommonUtils.GetConnectString());
        static readonly ProductRepository productRepository = new ProductRepository(CommonUtils.GetConnectString());

        public static async Task ADD_PRODUCT_TO_CART()
        {

            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine("**** AVAILABLE PRODUCTS ****");

                List<Product> PRODUCTS = await productRepository.GET_ALL_Async();

                foreach (Product product in PRODUCTS)
                {
                    Console.WriteLine($"ProductId: {product.ProductId}, Name: {product.Product_Name}, Price: {product.Price}, Inventory: {product.Inventory}, Category: {product.Category}");
                }

                Guid productId;
                do
                {

                    Console.WriteLine($"{Environment.NewLine}Enter PRODUCT ID add to CART ");
                    // Check if the input is null or whitespace
                    string rdx_ProductId = Console.ReadLine();


                    if (!Guid.TryParse(rdx_ProductId, out productId))
                    {
                        Console.WriteLine("Invalid product id.");
                    }
                } while (productId == Guid.Empty);

                Product existingProduct = await productRepository.GET_BY_ID_Async(productId);
                if (existingProduct == null)
                {
                    Console.WriteLine("Can't find existed Product");

                }
                else
                {

                    Console.WriteLine($"ProductId: {existingProduct.ProductId}, Name: {existingProduct.Product_Name}, Price: {existingProduct.Price}, Quantity: {existingProduct.Inventory}, Category: {existingProduct.Category}");

                    if (existingProduct.Inventory > 0)
                    {
                        Console.WriteLine("How many quantity product item do you want to add to CART");
                        int quantityProduct;
                        do
                        {
                            Console.Write("Enter quantity (must be greater than 0): ");
                            string rdx_Inventory = Console.ReadLine();

                            if (int.TryParse(rdx_Inventory, out quantityProduct) && quantityProduct > 0 && quantityProduct <= existingProduct.Inventory)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid quantity. Please enter a valid positive numeric value.");
                            }
                        } while (true);


                        Guid currentUser = HomeController.CURRENT_USER_ID();
                        int chckExistingCart = await cartRepository.CHECK_EXIST(currentUser);

                        CartDetail cartItem = new CartDetail();

                        if (chckExistingCart == 0)
                        {
                            Guid newCartId = Guid.NewGuid();
                            Console.WriteLine("Can't find existed Cart");
                            Cart newCart = new Cart()
                            {
                                CartId = newCartId,
                                UserId = currentUser,
                            };

                            ResponseMessage response_addCart = await cartRepository.ADD_Async(newCart);
                            if (response_addCart == null || !response_addCart.IsSuccess)
                            {
                                Console.WriteLine("Can't add new cart to user");
                            }
                            cartItem = new CartDetail
                            {
                                CartId = newCartId,
                                ProductId = existingProduct.ProductId,
                                Quantity = quantityProduct,
                            };

                        }
                        else

                        {
                            Cart existingCart = await cartRepository.GET_BY_ID_Async(currentUser);
                            if (existingCart == null)
                            {
                                Console.WriteLine("Can't find existed Cart");

                            }
                            else
                            {
                                int checkExistingProductInCart = await cartRepository.CHECK_EXIST_PRODUCT_IN_CART_BY_ID_Async(existingCart.CartId, existingProduct.ProductId);
                                ResponseMessage response = new ResponseMessage();
                                if (checkExistingProductInCart == 0)
                                {
                                    cartItem = new CartDetail
                                    {
                                        CartId = existingCart.CartId,
                                        ProductId = existingProduct.ProductId,
                                        Quantity = quantityProduct,
                                    };

                                    response = await cartRepository.ADD_CART_ITEMS_Async(cartItem);
                                }
                                else
                                {
                                    ProductInCartViewModel ExistProductInCart = await cartRepository.GET_PRODUCT_IN_CART_BY_ID_Async(existingCart.CartId, existingProduct.ProductId);

                                    CartDetail UpdateCartItem = new CartDetail()
                                    {
                                        CartId = existingCart.CartId,
                                        ProductId = existingProduct.ProductId,
                                        Quantity = ExistProductInCart.Quantity + quantityProduct
                                    };

                                    response = await cartRepository.UPDATE_PRODUCT_IN_CART_BY_ID_Async(UpdateCartItem);

                                }

                                if (response != null && response.IsSuccess)
                                {
                                    Product updateProduct = new Product()
                                    {

                                        Inventory = existingProduct.Inventory - quantityProduct,
                                        Product_Name = existingProduct.Product_Name,
                                        Price = existingProduct.Price,
                                        Category = existingProduct.Category

                                    };


                                    ResponseMessage responseUpdateProductInventory = await productRepository.UPDATE_Async(existingProduct.ProductId, updateProduct);
                                    if (responseUpdateProductInventory != null && responseUpdateProductInventory.IsSuccess)
                                    {
                                        Console.WriteLine("Add product to Cart successfully, return back to home");
                                    }

                                }

                            }

                        }

                    }


                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");

            }
            finally
            {
                await HomeController.Index();
            }

        }

        public static async Task VIEW_ALL_PRODUCTS_IN_CART()
        {
            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine("**** AVAILABLE PRODUCTS IN CARTS ****");

                Guid currentUser = HomeController.CURRENT_USER_ID();
                int chckExistingCart = await cartRepository.CHECK_EXIST(currentUser);
                Guid newCartId = Guid.NewGuid();
                if (chckExistingCart == 0)
                {
                    Console.WriteLine("Can't find existed Cart");
                    Cart newCart = new Cart()
                    {
                        CartId = newCartId,
                        UserId = currentUser,
                    };

                    ResponseMessage response_addCart = await cartRepository.ADD_Async(newCart);
                    if (response_addCart == null || !response_addCart.IsSuccess)
                    {
                        Console.WriteLine("Can't add new cart to user");
                    }

                }
                else
                {
                    Cart ExistingCart = await cartRepository.GET_BY_ID_Async(currentUser);
                    List<ProductInCartViewModel> PRODUCTS_IN_CART = await cartRepository.GET_PRODUCTS_IN_CART_Async(ExistingCart.CartId);



                    // Check if there are products in the cart
                    if (PRODUCTS_IN_CART.Count == 0)
                    {
                        Console.WriteLine("No products in the cart.");
                        return;
                    }
                    else
                    {
                        foreach (ProductInCartViewModel productItem in PRODUCTS_IN_CART)
                        {
                            Console.WriteLine($"Id: {productItem.Id}, Product Name: {productItem.Product_Name}, Price: {productItem.Price}, Quantity: {productItem.Quantity}, Category: {productItem.Category}");
                        }
                        // Calculate and display the total price
                        decimal totalPrice = PRODUCTS_IN_CART.Sum(product => product.Price);
                        Console.WriteLine($"Total Price: {totalPrice}");


                    }


                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");

            }
            finally
            {
                await HomeController.Index();
            }
        }

        public static async Task DELETE_PRODUCT_IN_CART()
        {
            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine("**** DELETE PRODUCT FROM CART ****");

                // Get the current user ID
                Guid currentUser = HomeController.CURRENT_USER_ID();
                Cart existingCart = await cartRepository.GET_BY_ID_Async(currentUser);
                if (existingCart == null)
                {
                    Console.WriteLine("Can't find existed Cart");

                }
                else
                {
                    List<ProductInCartViewModel> PRODUCTS_IN_CART = await cartRepository.GET_PRODUCTS_IN_CART_Async(existingCart.CartId);

                    // Check if there are products in the cart
                    if (PRODUCTS_IN_CART.Count == 0)
                    {
                        Console.WriteLine("No products in the cart.");
                        return;
                    }
                    else
                    {
                        foreach (ProductInCartViewModel productItem in PRODUCTS_IN_CART)
                        {
                            Console.WriteLine($"Id: {productItem.Id}, ProductId: {productItem.ProductId} , Product Name: {productItem.Product_Name}, Price: {productItem.Price}, Quantity: {productItem.Quantity}, Category: {productItem.Category}");
                        }
                        // Calculate and display the total price
                        decimal totalPrice = PRODUCTS_IN_CART.Sum(product => product.Price);
                        Console.WriteLine($"Total Price: {totalPrice}");

                    }
                    // Ask the user to enter the ProductId to delete
                    Console.WriteLine($"{Environment.NewLine}Enter PRODUCT ID to delete from CART ");
                    Guid productIdToDelete;
                    do
                    {
                        Console.WriteLine("Enter Product Id>");
                        // Check if the input is null or whitespace
                        string rdx_ProductIdToDelete = Console.ReadLine();

                        if (Guid.TryParse(rdx_ProductIdToDelete, out productIdToDelete))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid product id. Please try again.");
                        }
                    } while (true);


                    int InventoryProduct = 0;
                    int QuantityProductInCart = 0;

                    Product ExistProduct = await productRepository.GET_BY_ID_Async(productIdToDelete);
                    if (ExistProduct != null)
                    {
                        InventoryProduct = ExistProduct.Inventory;
                        ProductInCartViewModel ExistProductInCart = PRODUCTS_IN_CART.Where(t => t.ProductId == productIdToDelete).FirstOrDefault();
                        if (ExistProductInCart != null)
                        {
                            QuantityProductInCart = ExistProductInCart.Quantity;
                        }

                        Product updateProduct = new Product()
                        {

                            Inventory = InventoryProduct + QuantityProductInCart,
                            Product_Name = ExistProduct.Product_Name,
                            Price = ExistProduct.Price,
                            Category = ExistProduct.Category

                        };
                        ResponseMessage response = await productRepository.UPDATE_Async(productIdToDelete, updateProduct);
                        if (response != null && response.IsSuccess)
                        {
                            ResponseMessage responseDelete = await cartRepository.DELETE_PRODUCTS_IN_CART_Async(productIdToDelete);
                            if (responseDelete != null && responseDelete.IsSuccess)
                            {
                                Console.WriteLine($"Delect Prodcut In Cart sucessfully ");
                            }

                        }
                        else
                        {
                            Console.WriteLine($"Product updated failed");

                        }

                    }
                }
            }


            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            finally
            {

                await HomeController.Index();
            }


        }







    }


}

