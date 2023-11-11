using Basic_CSharp.Controllers;

namespace Basic_CSharp
{
    public class Program
    {

        private static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            await HomeController.Index();

        }



    }


}
