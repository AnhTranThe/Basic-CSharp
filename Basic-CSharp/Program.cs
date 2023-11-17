using Basic_CSharp.Controllers;

namespace Basic_CSharp
{
    public class Program
    {
        /// <summary>
        /// Project first start
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            await HomeController.Index();

        }

    }


}
