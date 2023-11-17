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


        public static async Task VIEW_ALL_USER_ORDERS()
        {
            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine("**** AVAILABLE USER ORDERS ****");

                Guid currentUser = HomeController.CURRENT_USER_ID();
                int chckExistingOrderUser = orderRepository.CHECK_EXIST(currentUser);

                if (chckExistingOrderUser == 0)
                {
                    Console.WriteLine("Can't find existed Order User");

                }
                else
                {

                    List<ProductInOrderViewModel> ProductInOrders = await orderRepository.GET_PRODUCTS_IN_ORDERS_Async(currentUser);

                    // Check if there are products in the cart
                    if (ProductInOrders.Count == 0)
                    {
                        Console.WriteLine("No products in the order.");
                        return;
                    }
                    else
                    {
                        foreach (ProductInOrderViewModel productItem in ProductInOrders)
                        {
                            Console.WriteLine($"Order Id: {productItem.OrderId}, " +
                                             $"Product Id: {productItem.ProductId}, " +
                                             $"Name: {productItem.Product_Name}, " +
                                             $"Price: {productItem.Price}, " +
                                             $"Quantity: {productItem.Quantity}, " +
                                             $"Category: {productItem.Category}");
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

        public static async Task ADD_PRODUCT_CART_TO_USER_ORDER()
        {

            try
            {
                Guid currentUser = HomeController.CURRENT_USER_ID();
                int chckExistingCart = cartRepository.CHECK_EXIST(currentUser);

                Guid newOrderId = Guid.NewGuid();
                if (chckExistingCart == 0)
                {
                    Console.WriteLine("Can't find existed Cart");
                }
                else
                {
                    Cart ExistingCart = await cartRepository.GET_BY_ID_Async(currentUser);
                    if (ExistingCart != null)
                    {
                        List<ProductInCartViewModel> PRODUCTS_IN_CART = await cartRepository.GET_PRODUCTS_IN_CART_Async(ExistingCart.CartId);



                        if (PRODUCTS_IN_CART != null && PRODUCTS_IN_CART.Any())
                        {
                            decimal totalPrice = PRODUCTS_IN_CART.Sum(product => product.Price);
                            Order newOrder = new Order()
                            {
                                OrderId = newOrderId,
                                UserId = currentUser,
                                Amount = totalPrice,
                                OrderDate = DateTime.Now,

                            };
                            ResponseMessage responseAddOrder = await orderRepository.ADD_CART_TO_ORDER_DETAIL_Async(newOrder, PRODUCTS_IN_CART);
                            if (responseAddOrder != null && responseAddOrder.IsSuccess)
                            {
                                Console.WriteLine("Add Order successfully !");

                            }
                            else
                            {
                                Console.WriteLine("Can't add Cart to Order");


                            }


                        }
                        else
                        {
                            Console.WriteLine("There is no Product items in Cart");
                        }

                    }
                    else
                    {
                        Console.WriteLine("User Cart no existed");
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



        public static async Task DELETE_USER_ORDER()
        {
            try
            {
                // Display available orders to the user
                await VIEW_ALL_USER_ORDERS(); // Implement VIEW_ALL_ORDERS as per your requirements

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
