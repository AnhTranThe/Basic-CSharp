using Basic_CSharp.Models;
using Basic_CSharp.Repositories;
using Basic_CSharp.Utilities;

namespace Basic_CSharp.Controllers
{
    public static class UserController
    {
        readonly static UserRepository userRepository = new UserRepository(CommonUtils.GetConnectString());

        public static async Task SIGN_IN_USER(string loggedInUserId)
        {

            try
            {
                ResponseMessage RESPONSE = await userRepository.LOGIN_USER_Async(loggedInUserId);

                if (RESPONSE != null && RESPONSE.Message != "")
                {
                    loggedInUserId = RESPONSE.Message;
                    await HomeController.Index();
                }
                else
                {
                    Console.WriteLine("Log in fail. No user email found, please try again !");
                    await HomeController.Index();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }



        }

        public static async Task SIGN_UP_USER()
        {


        }


        public static async Task SIGN_OUT_USER()
        {


        }

    }
}
