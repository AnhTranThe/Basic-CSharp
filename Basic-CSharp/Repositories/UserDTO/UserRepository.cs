using Basic_CSharp.Models;
using System.Data.SqlClient;


namespace Basic_CSharp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }


        // METHOD IMPLEMENTATIONS
        public async Task<List<User>> GET_ALL_USERS_Async()
        {
            //List<User> userLs = new List<User>();
            //try
            //{

            //    SqlCommand cmd = new SqlCommand("SELECT * FROM USERS", connection);
            //    cmd.CommandType = CommandType.Text;
            //    connection.Open();
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    while (rdr.Read())
            //    {
            //        userLs.Add(new User
            //        {
            //            UserId = rdr.GetGuid("UserId"),
            //            First_Name = rdr.GetString("First_Name"),
            //            Last_Name = rdr.GetString("Last_Name"),
            //            Dob = rdr.GetDateTime("Dob")
            //        });

            //    }
            //    connection.Close();

            //    foreach (User user in userLs)
            //    {

            //        Console.WriteLine(user.Last_Name + " " + user.First_Name);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}



            throw new NotImplementedException();
        }

        public async Task<User> GET_USER_Async()
        {
            Console.WriteLine($"{Environment.NewLine}Please select User Id you want to view:");
            Console.Write("Enter User ID: ");
            string? viewUserId = Console.ReadLine();
            Guid guid_viewUserId = new Guid();
            if (String.IsNullOrWhiteSpace(viewUserId))
            {
                guid_viewUserId = Guid.Parse(viewUserId);

            }

            throw new NotImplementedException();

        }

        public async Task<ResponseMessage> ADD_USER_Async()
        {

            Console.Clear();
            Console.WriteLine($"{Environment.NewLine}Please field information to add new user");
            Console.Write("Enter First Name: ");
            string? firstname_User = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string? lastname_User = Console.ReadLine();
            Console.Write("Enter DOB: ");
            string? DOB_User = Console.ReadLine();
            Console.Write("Enter Email: ");
            string? Email_User = Console.ReadLine();
            Console.WriteLine("Select Gender options below:");
            Console.WriteLine("1. Male >");
            Console.WriteLine("2. Female >");
            Console.WriteLine("3. Other >");
            string? rdx_GENDER_SELECTED_OPTION = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(rdx_GENDER_SELECTED_OPTION))
            {
                string? Gender_User = null;
                switch (rdx_GENDER_SELECTED_OPTION)
                {
                    case "1":
                        Gender_User = "Male";
                        break;
                    case "2":
                        Gender_User = "Female";
                        break;
                    case "3":
                        Gender_User = "Other";
                        break;
                }
            }
            else
            {

            }
            throw new NotImplementedException();

        }

        public async Task<ResponseMessage> UPDATE_USER_Async()
        {
            Console.WriteLine($"{Environment.NewLine}Please select User Id you want to update:");
            Console.Write("Enter User ID: ");
            string? updateUserId = Console.ReadLine();
            Guid guid_UpdateUserId = new Guid();
            if (String.IsNullOrWhiteSpace(updateUserId))
            {
                guid_UpdateUserId = Guid.Parse(updateUserId);

            }
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> DELETE_USER_Async()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage> LOGIN_USER_Async(string loggedInUserId)
        {

            try
            {
                Console.WriteLine($"{Environment.NewLine}Please field your email account");

                Console.Write("Enter Email: ");
                string? email = Console.ReadLine();

                SqlConnection connection = new SqlConnection(_connectionString);
                connection.StatisticsEnabled = true;
                connection.FireInfoMessageEventOnUserErrors = true;
                connection.Open();
                string selectQuery = "SELECT UserId FROM USERS WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {

                    command.Parameters.AddWithValue("@Email", email);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        loggedInUserId = result.ToString();

                        return new ResponseMessage
                        {
                            Message = $"{loggedInUserId}"
                        };
                    }

                }
                connection.Close();
                return new ResponseMessage
                {
                    Message = ""
                };


            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured: {ex.Message}");
            }


        }

        public async Task<ResponseMessage> LOGOUT_USER_Async(string loggedInUserId)
        {
            loggedInUserId = "";
            return new ResponseMessage
            {
                Message = $"{loggedInUserId}"
            };

        }
    }
}
