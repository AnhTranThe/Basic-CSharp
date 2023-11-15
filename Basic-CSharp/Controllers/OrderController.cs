using Basic_CSharp.Models;
using Basic_CSharp.Repositories;
using Basic_CSharp.Utilities;
using Basic_CSharp.ViewModels;

namespace Basic_CSharp.Controllers
{
    public static class OrderController
    {
        static readonly CartRepository cartRepository = new CartRepository(CommonUtils.GetConnectString());
        static readonly OrderRepository orderRepository = new OrderRepository(CommonUtils.GetConnectString());

        public static async Task VIEW_ALL_ORDERS()
        {

        }

        public static async Task ADD_PRODUCT_CART_TO_ORDER()
        {

            try
            {
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
                        // Ask the user if they want to proceed with adding the products to an order
                        Console.WriteLine("Do you want to proceed with adding these products to an order? (1. Yes, 2. No)");

                        string userInput = Console.ReadLine();
                        if (userInput == "1")
                        {

                            ResponseMessage orderResonse = await orderRepository.ADD_ORDER_FROM_CART_ITEMS_Async(PRODUCTS_IN_CART, totalPrice, currentUser);
                            if (orderResonse != null && orderResonse.IsSuccess)
                            {


                                ResponseMessage cartResonse = await cartRepository.DELETE_PRODUCTS_IN_CART_Async(ExistingCart.CartId);
                                Console.WriteLine("Products added to the order successfully.");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Operation canceled.");
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

        public static async Task DELETE_ORDER()
        {
            try
            {
                // Display available orders to the user
                await VIEW_ALL_ORDERS(); // Implement VIEW_ALL_ORDERS as per your requirements

                // Ask the user to enter the OrderId to delete
                Console.WriteLine($"{Environment.NewLine}Enter ORDER ID to delete");
                Guid orderIdToDelete;
                do
                {
                    Console.WriteLine("Enter Order Id>");
                    // Check if the input is null or whitespace
                    string rdx_OrderIdToDelete = Console.ReadLine();

                    if (Guid.TryParse(rdx_OrderIdToDelete, out orderIdToDelete))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid order id. Please try again.");
                    }
                } while (true);

                // Call a method in your repository to delete the order
                ResponseMessage response = await orderRepository.DELETE_Async(orderIdToDelete);

                if (response.IsSuccess)
                {
                    Console.WriteLine("Order deleted successfully.");

                }
                else
                {
                    Console.WriteLine(response.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            finally
            {
                // Redirect to the admin menu or perform any other necessary actions
            }

        }
    }
}
