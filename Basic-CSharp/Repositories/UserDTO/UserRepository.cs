using Basic_CSharp.Models;
using Basic_CSharp.Utilities;
using System.Data.SqlClient;


namespace Basic_CSharp.Repositories
{
    public class UserRepository : IUserRepository
    {

        public string ConnectionString { get; set; }

        public UserRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        // METHOD IMPLEMENTATIONS
        public async Task<List<User>> GET_ALL_Async()
        {

            List<User> userLs = new List<User>();
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            try
            {



                string selectQuery = $"SELECT * FROM USERS";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlDataReader dataReader = await command.ExecuteReaderAsync();

                while (dataReader.Read())
                {

                    User row_user = new User();
                    int index_UserId = CommonUtils.GetIntFromDataReader(dataReader, "UserId");
                    int index_First_Name = CommonUtils.GetIntFromDataReader(dataReader, "First_Name");
                    int index_Last_Name = CommonUtils.GetIntFromDataReader(dataReader, "Last_Name");
                    int index_Dob = CommonUtils.GetIntFromDataReader(dataReader, "Dob");
                    int index_Full_Name = CommonUtils.GetIntFromDataReader(dataReader, "Full_Name");
                    int index_Email = CommonUtils.GetIntFromDataReader(dataReader, "Email");
                    int index_Gender = CommonUtils.GetIntFromDataReader(dataReader, "Gender");


                    // Access columns by name or index
                    row_user.UserId = !dataReader.IsDBNull(index_UserId) ? dataReader.GetGuid(index_UserId) : Guid.Empty;
                    row_user.First_Name = !dataReader.IsDBNull(index_First_Name) ? dataReader.GetString(index_First_Name) : string.Empty;
                    row_user.Last_Name = !dataReader.IsDBNull(index_Last_Name) ? dataReader.GetString(index_Last_Name) : string.Empty;
                    row_user.Dob = !dataReader.IsDBNull(index_Dob) ? dataReader.GetDateTime(index_Dob) : DateTime.MinValue;
                    row_user.Full_Name = !dataReader.IsDBNull(index_Full_Name) ? dataReader.GetString(index_Full_Name) : string.Empty;
                    row_user.Email = !dataReader.IsDBNull(index_Email) ? dataReader.GetString(index_Email) : string.Empty;
                    row_user.Gender = !dataReader.IsDBNull(index_Gender) ? dataReader.GetString(index_Gender) : string.Empty;

                    userLs.Add(row_user);
                }
                dataReader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();

            }
            return userLs;

        }

        public async Task<User> GET_BY_ID_Async(Guid Id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            User Chk_User = new User();
            try
            {


                string selectQuery = $"SELECT * FROM USERS WHERE UserId = @userId ";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@userId", Id);
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    int index_UserId = CommonUtils.GetIntFromDataReader(dataReader, "UserId");
                    int index_First_Name = CommonUtils.GetIntFromDataReader(dataReader, "First_Name");
                    int index_Last_Name = CommonUtils.GetIntFromDataReader(dataReader, "Last_Name");
                    int index_Dob = CommonUtils.GetIntFromDataReader(dataReader, "Dob");
                    int index_Full_Name = CommonUtils.GetIntFromDataReader(dataReader, "Full_Name");
                    int index_Email = CommonUtils.GetIntFromDataReader(dataReader, "Email");
                    int index_Gender = CommonUtils.GetIntFromDataReader(dataReader, "Gender");


                    // Access columns by name or index
                    Chk_User.UserId = !dataReader.IsDBNull(index_UserId) ? dataReader.GetGuid(index_UserId) : Guid.Empty;
                    Chk_User.First_Name = !dataReader.IsDBNull(index_First_Name) ? dataReader.GetString(index_First_Name) : string.Empty;
                    Chk_User.Last_Name = !dataReader.IsDBNull(index_Last_Name) ? dataReader.GetString(index_Last_Name) : string.Empty;
                    Chk_User.Dob = !dataReader.IsDBNull(index_Dob) ? dataReader.GetDateTime(index_Dob) : DateTime.MinValue;
                    Chk_User.Full_Name = !dataReader.IsDBNull(index_Full_Name) ? dataReader.GetString(index_Full_Name) : string.Empty;
                    Chk_User.Email = !dataReader.IsDBNull(index_Email) ? dataReader.GetString(index_Email) : string.Empty;
                    Chk_User.Gender = !dataReader.IsDBNull(index_Gender) ? dataReader.GetString(index_Gender) : string.Empty;

                }
                dataReader.Close();


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {

                connection.Close();
            }
            return Chk_User;

        }

        public async Task<ResponseMessage> ADD_Async(User newUser)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {

                string insertQuery = "INSERT INTO USERS (UserId, First_Name, Last_Name, Dob, Full_Name, Email, Gender) " +
                    "VALUES (@userId, @firstName, @lastName, @dob, @fullName, @email, @gender)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@userId", newUser.UserId);
                command.Parameters.AddWithValue("@firstName", newUser.First_Name);
                command.Parameters.AddWithValue("@lastName", newUser.Last_Name);
                command.Parameters.AddWithValue("@dob", newUser.Dob);
                command.Parameters.AddWithValue("@fullName", newUser.Full_Name);
                command.Parameters.AddWithValue("@email", newUser.Email);
                command.Parameters.AddWithValue("@gender", newUser.Gender);

                int result = await command.ExecuteNonQueryAsync();

                if (result == 0)
                {

                    return new ResponseMessage
                    {
                        IsSuccess = false

                    };

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();

            }
            return new ResponseMessage
            {
                IsSuccess = true

            };

        }

        public async Task<ResponseMessage> UPDATE_Async(Guid Id, User ModifiedUser)
        {

            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {


                string updateQuery = "UPDATE USERS SET " +
                                    "First_Name = @firstName, Last_Name = @lastName," +
                                    "Dob = @dob , Full_Name = @fullName , Email = @email, Gender = @gender" +
                                    " WHERE UserId = @userId";


                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@userId", ModifiedUser.UserId);
                command.Parameters.AddWithValue("@firstName", ModifiedUser.First_Name);
                command.Parameters.AddWithValue("@lastName", ModifiedUser.Last_Name);
                command.Parameters.AddWithValue("@dob", ModifiedUser.Dob);
                command.Parameters.AddWithValue("@fullName", ModifiedUser.Full_Name);
                command.Parameters.AddWithValue("@email", ModifiedUser.Email);
                command.Parameters.AddWithValue("@gender", ModifiedUser.Gender);


                int result = await command.ExecuteNonQueryAsync();

                if (result == 0)
                {

                    return new ResponseMessage
                    {
                        IsSuccess = false

                    };

                }




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            finally
            {
                connection.Close();
            }
            return new ResponseMessage
            {
                IsSuccess = true

            };
        }

        public async Task<ResponseMessage> DELETE_Async(Guid Id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {
                string deleteQuery = "DELETE FROM USERS WHERE UserId = @userId";

                SqlCommand command = new SqlCommand(deleteQuery, connection);

                command.Parameters.AddWithValue("@userId", Id);

                int result = await command.ExecuteNonQueryAsync();

                if (result == 0)
                {

                    return new ResponseMessage
                    {
                        IsSuccess = false

                    };

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            finally
            {
                connection.Close();
            }
            return new ResponseMessage
            {
                IsSuccess = true

            };

        }

        public ResponseMessage LOGIN_USER()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            try
            {
                Console.WriteLine($"{Environment.NewLine}Please field your email account");
                Console.Write("Enter Email: ");
                string? email = Console.ReadLine();


                string selectQuery = $"SELECT UserId FROM USERS WHERE Email = @email";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@email", email);
                object result = command.ExecuteScalar();

                if (result != null && Guid.TryParse(result.ToString(), out Guid ExistedGuidUserId))
                {

                    return new ResponseMessage
                    {
                        LogInUserId = ExistedGuidUserId
                    };
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            finally { connection.Close(); }

            return new ResponseMessage
            {
                LogInUserId = Guid.Empty
            };

        }

        public ResponseMessage LOGOUT_USER()
        {

            return new ResponseMessage
            {
                LogInUserId = Guid.Empty
            };

        }

        public int CHECK_EXIST(Guid Id)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                connection.Open();
                string selectQuery = "SELECT COUNT(*) FROM USERS WHERE UserId = @UserId";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@UserId", Id);

                object result = command.ExecuteScalarAsync();

                int cartCount = result != null ? Convert.ToInt32(result) : 0;
                connection.Close();
                return cartCount;



            }

        }

    }
}
