using Basic_CSharp.Utilities;

namespace Basic_CSharp.Controllers
{
    public static class HomeController
    {
        public static Guid loggedInUserId = Guid.Empty; // Keep track of the logged-in user
        public static async Task Index()
        {

            while (true)
            {

                if (loggedInUserId == Guid.Empty)
                {

                    Console.WriteLine($"{Environment.NewLine}Welcome to Basic-CSharp-ConsoleApp-Ecommerce");
                    Console.WriteLine($"{Environment.NewLine}Please select an option to proceed....");

                    Console.WriteLine("1. Sign Up ");
                    Console.WriteLine("2. Sign In ");
                    Console.WriteLine("0. Exit");

                    string? rdx_SELECTED_OPTION;
                    int int_rdx_SELECTED_OPTION;

                    do
                    {
                        rdx_SELECTED_OPTION = Console.ReadLine();

                        if (String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION))
                        {

                            Console.WriteLine("select option cannot be empty. Please try again.");
                        }
                        else
                        {
                            if (!Int32.TryParse(rdx_SELECTED_OPTION, out int_rdx_SELECTED_OPTION))
                            {
                                Console.WriteLine("Must select number option from 0 to 2");

                            }

                            else
                            {

                                switch (int_rdx_SELECTED_OPTION)
                                {
                                    case 0:
                                        await HomeController.Index();
                                        break;
                                    case 1:
                                        Console.Clear();
                                        await UserController.SIGN_UP_USER();
                                        break;
                                    case 2:
                                        loggedInUserId = UserController.SIGN_IN_USER();
                                        break;

                                    default:

                                        Console.WriteLine("Rerunning main logic...");
                                        Console.Clear();
                                        await Index();
                                        break;

                                }

                            }

                        }


                    }
                    while (rdx_SELECTED_OPTION != null && String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION));

                }
                else
                {
                    if (loggedInUserId == new Guid(ConstSystem.Admin_UserId.ToLower()))
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
                                    Console.WriteLine($"{Environment.NewLine} Choose which objects to select");
                                    Console.WriteLine("1. CRUD USERS >");
                                    Console.WriteLine("2. CRUD PRODUCTS >");
                                    //Console.WriteLine("3. CRUD CARTS >");
                                    //Console.WriteLine("4. CRUD ORDERS >");
                                    Console.WriteLine("0. EXIT >");

                                    // RECEIVE USER INPUT
                                    string rdx_SELECTED_OPTION_1 = string.Empty;


                                    do
                                    {
                                        rdx_SELECTED_OPTION_1 = Console.ReadLine();

                                        if (String.IsNullOrEmpty(rdx_SELECTED_OPTION_1))
                                        {

                                            Console.WriteLine("select Product option cannot be empty. Please try again.");
                                        }
                                        else
                                        {

                                            await REDIRECT_TO_SELECTED_OPTION_ADMIN(rdx_SELECTED_OPTION_1);


                                        }


                                    }
                                    while (rdx_SELECTED_OPTION_1 != null && String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION_1));

                                    break;


                                case "0":
                                    loggedInUserId = UserController.SIGN_OUT_USER();
                                    Console.Clear();
                                    await Index();
                                    break;

                            }


                        }
                    }
                    else
                    {

                        Console.WriteLine($"{Environment.NewLine}Please select an option to proceed....");
                        Console.WriteLine("1. ADD PRODUCT ITEM TO CART");
                        Console.WriteLine("2. VIEW ALL PRODUCTS IN CART");
                        Console.WriteLine("3. DELETE PRODUCT ITEM IN CART");
                        Console.WriteLine("4. ORDER PRODUCTS FROM CART");
                        Console.WriteLine("5. DELETE ORDER PRODUCTS");
                        Console.WriteLine("0. LOGOUT");


                        // RECEIVE USER INPUT

                        string? rdx_SELECTED_OPTION;
                        int int_rdx_SELECTED_OPTION;

                        do
                        {

                            rdx_SELECTED_OPTION = Console.ReadLine();
                            Console.WriteLine($"{Environment.NewLine}Selected option: {rdx_SELECTED_OPTION}{Environment.NewLine}");

                            if (String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION))
                            {

                                Console.WriteLine("select option cannot be empty. Please try again.");
                            }
                            else if (!Int32.TryParse(rdx_SELECTED_OPTION, out int_rdx_SELECTED_OPTION))
                            {
                                Console.WriteLine("Must select number option from 0 to 5");
                            }

                            else
                            {

                                await REDIRECT_TO_SELECTED_OPTION_GUEST(int_rdx_SELECTED_OPTION);

                            }


                        }
                        while (rdx_SELECTED_OPTION != null && String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION));



                    }


                }

            }

        }
        public static Guid CURRENT_USER_ID()
        {
            return loggedInUserId;
        }

        public static async Task REDIRECT_TO_SELECTED_OPTION_ADMIN(string rdx_SELECTED_OPTION)
        {
            Console.WriteLine($"{Environment.NewLine}Selected option: {rdx_SELECTED_OPTION}");
            int int_rdx_SELECTED_OPTION = Convert.ToInt32(rdx_SELECTED_OPTION);
            switch (int_rdx_SELECTED_OPTION)
            {
                case 1:
                    // DISPLAY OPTIONS USERS
                    Console.WriteLine("1. Add a user >");
                    Console.WriteLine("2. View all users >");
                    Console.WriteLine("3. View a user >");
                    Console.WriteLine("4. Update a user >");
                    Console.WriteLine("5. Delete a user >");
                    Console.WriteLine("0. Exit >");

                    string? rdx_SELECTED_OPTION_USER = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION_USER))
                    {

                        switch (rdx_SELECTED_OPTION_USER)
                        {
                            case "1":
                                await UserController.SIGN_UP_USER();
                                break;
                            case "2":
                                await UserController.VIEW_USERS_ALL();
                                break;
                            case "3":
                                await UserController.VIEW_USER();
                                break;
                            case "4":
                                await UserController.UPDATE_USER();
                                break;
                            case "5":
                                await UserController.DELETE_USER();
                                break;

                            case "0":
                                Console.Clear();
                                await Index();
                                break;

                        }

                    }

                    break;
                case 2:
                    // DISPLAY OPTIONS PRODUCTS
                    Console.WriteLine("1. Add a product >");
                    Console.WriteLine("2. View all products >");
                    Console.WriteLine("3. View a product >");
                    Console.WriteLine("4. Update a product >");
                    Console.WriteLine("5. Delete a product >");
                    Console.WriteLine("0. Exit >");

                    string? rdx_SELECTED_OPTION_PRODUCT = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(rdx_SELECTED_OPTION_PRODUCT))
                    {

                        switch (rdx_SELECTED_OPTION_PRODUCT)
                        {
                            case "1":
                                await ProductController.ADD_PRODUCT();
                                break;
                            case "2":
                                await ProductController.VIEW_PRODUCTS_ALL();
                                break;
                            case "3":
                                await ProductController.VIEW_PRODUCT();
                                break;
                            case "4":
                                await ProductController.UPDATE_PRODUCT();
                                break;
                            case "5":
                                await ProductController.DELETE_PRODUCT();
                                break;

                            case "0":
                                Console.Clear();
                                await Index();
                                break;

                        }

                    }

                    break;

                case 3:
                    // DISPLAY OPTIONS CARTS
                    Console.WriteLine("1. Add a cart >");
                    Console.WriteLine("2. View all carts >");
                    Console.WriteLine("3. View a cart >");
                    Console.WriteLine("4. Update a cart >");
                    Console.WriteLine("5. Delete a cart >");
                    Console.WriteLine("0. Exit >");
                    break;

                case 4:
                    // DISPLAY OPTIONS CARTS
                    Console.WriteLine("1. Add a order >");
                    Console.WriteLine("2. View all orders >");
                    Console.WriteLine("3. View a order >");
                    Console.WriteLine("4. Update a order >");
                    Console.WriteLine("5. Delete a order >");
                    Console.WriteLine("0. Exit >");
                    break;

                case 0:
                    Console.Clear();
                    await Index();
                    break;

            }
        }

        public static async Task REDIRECT_TO_SELECTED_OPTION_GUEST(int int_rdx_SELECTED_OPTION)
        {

            switch (int_rdx_SELECTED_OPTION)
            {
                case 0:
                    UserController.SIGN_OUT_USER();
                    Console.Clear();
                    await Index();
                    break;
                case 1:
                    ///1. ADD PRODUCT ITEM TO CART

                    await CartController.ADD_PRODUCT_TO_CART();

                    break;
                case 2:
                    ///2. VIEW ALL PRODUCTS IN CART

                    await CartController.VIEW_ALL_PRODUCTS_IN_CART();

                    break;
                case 3:
                    ///3. DELETE PRODUCT ITEM IN CART
                    await CartController.DELETE_PRODUCT_IN_CART();

                    break;


                case 4:
                    ///4. ORDER PRODUCTS FROM CART
                    await OrderController.ADD_PRODUCT_CART_TO_ORDER();

                    break;

                case 5:
                    ///5. DELETE ORDER PRODUCTS
                    await OrderController.DELETE_ORDER();

                    break;
                default:
                    Console.WriteLine("Invalid option. Please select 0 to 5");
                    break;

            }








        }

    }
}
