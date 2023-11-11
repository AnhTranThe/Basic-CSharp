using Microsoft.Extensions.Configuration;

namespace Basic_CSharp.Utilities
{
    public static class CommonUtils
    {
        public static string GetConnectString()
        {
            var configBuilder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())      // file config ở thư mục hiện tại
                       .AddJsonFile("appsetting.json");                    // nạp config định dạng JSON
            var configurationroot = configBuilder.Build();

            return configurationroot["ConnectionStrings:Default"];

        }

    }


}
