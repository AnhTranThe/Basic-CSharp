using Basic_CSharp.Models;

namespace Basic_CSharp.Controllers
{
    public static class HomeController
    {
        public static string loggedInUserId; // Keep track of the logged-in user
        public static async Task Index()
        {


            while (true)
            {
                if (String.IsNullOrWhiteSpace(loggedInUserId))
                {

                    Console.WriteLine($"{Environment.NewLine}Welcome to Basic-CSharp-ConsoleApp-Ecommerce");
                    Console.WriteLine($"{Environment.NewLine}Please select an option to proceed....");

                    Console.WriteLine("1. Sign Up ");
                    Console.WriteLine("2. Sign In ");
                    Console.WriteLine("0. Exit");

                    string? rdx_SELECTED_OPTION = Console.ReadLine();

                    if (!String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION))
                    {

                        await REDIRECT_TO_SELECTED_OPTION_FIRST(rdx_SELECTED_OPTION);

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Rerunning main logic...");
                        await Index();

                    }

                }
                else
                {
                    if (loggedInUserId == "59BC3721-4145-485B-AC86-795DDF119F1F".ToLower())
                    {
                        Console.WriteLine("1. CRUD 4 OBJECTS");
                        Console.WriteLine("0. Logout");
                        string? rdx_SELECTED_OPTION = Console.ReadLine();
                        if (!String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION))
                        {
                            switch (rdx_SELECTED_OPTION)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("1. Add a user >");
                                    Console.WriteLine("2. View all users >");
                                    Console.WriteLine("3. View a user >");
                                    Console.WriteLine("4. Update a user >");
                                    Console.WriteLine("5. Delete a user >");
                                    Console.WriteLine("6. Add a product >");
                                    Console.WriteLine("7. View all products >");
                                    Console.WriteLine("8. View a product >");
                                    Console.WriteLine("9. Update a product >");
                                    Console.WriteLine("10. Delete a product >");
                                    string? rdx_SELECTED_OPTION_1 = Console.ReadLine();
                                    if (!String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION_1))
                                    {
                                        await REDIRECT_TO_SELECTED_OPTION_ADMIN(rdx_SELECTED_OPTION_1);

                                    }

                                    break;
                                case "0":

                                    break;

                            }

                            await REDIRECT_TO_SELECTED_OPTION_ADMIN(rdx_SELECTED_OPTION);


                        }

                    }

                }

            }


        }

        private static async Task REDIRECT_TO_SELECTED_OPTION_FIRST(string rdx_SELECTED_OPTION)
        {
            Console.WriteLine($"{Environment.NewLine}Selected option: {rdx_SELECTED_OPTION}");
            switch (rdx_SELECTED_OPTION)
            {
                case "1":
                    Console.Clear();
                    await UserController.SIGN_UP_USER();

                    break;
                case "2":
                    Console.Clear();
                    await UserController.SIGN_IN_USER(loggedInUserId);
                    break;

                default:
                    await Index();
                    break;
            }
        }
        private static async Task REDIRECT_TO_SELECTED_OPTION_ADMIN(string rdx_SELECTED_OPTION)
        {
            Console.WriteLine($"{Environment.NewLine}Selected option: {rdx_SELECTED_OPTION}");

            switch (rdx_SELECTED_OPTION)
            {
                //case "1":
                //    await userRepository.ADD_USER_Async();
                //    break;
                //case "2":
                //    await userRepository.GET_ALL_USERS_Async();
                //    break;
                //case "3":
                //    await userRepository.GET_USER_Async();
                //    break;
                //case "4":
                //    await userRepository.UPDATE_USER_Async();
                //    break;
                //case "5":
                //    await userRepository.DELETE_USER_Async();
                //    break;


                case "6":
                    await ProductController.ADD_PRODUCT();
                    break;
                case "7":
                    await ProductController.VIEW_PRODUCTS_ALL();
                    break;
                case "8":
                    await ProductController.GET_PRODUCT_Async();
                    break;
                case "9":
                    await productRepository.UPDATE_PRODUCT_Async();
                    break;
                case "10":
                    await productRepository.DELETE_PRODUCT_Async();
                    break;
                case "0":

                    ResponseMessage responseMessage = await userRepository.LOGOUT_USER_Async(loggedInUserId);
                    if (responseMessage != null && responseMessage.Message == "")
                    {
                        loggedInUserId = responseMessage.Message;
                        await Index();
                    }


                    break;
                default:
                    await Index();
                    break;
            }
        }

    }
}
