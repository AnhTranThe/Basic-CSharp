using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

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


        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static int GetIntFromDataReader(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return columnIndex;
        }

    }


}
