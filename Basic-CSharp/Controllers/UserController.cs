using Basic_CSharp.Models;
using Basic_CSharp.Repositories;
using Basic_CSharp.Utilities;

namespace Basic_CSharp.Controllers
{
    public static class UserController
    {
        readonly static UserRepository userRepository = new UserRepository(CommonUtils.GetConnectString());
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Guid SIGN_IN_USER()
        {
            try
            {
                ResponseMessage RESPONSE = userRepository.LOGIN_USER();
                return RESPONSE.LogInUserId;
            }
            catch (Exception e)
            {

                return Guid.Empty;
            }

        }

        public static async Task SIGN_UP_USER()
        {

            Console.WriteLine("Please add information about new user >");
            string firstName = string.Empty;
            string lastName = string.Empty;
            DateTime Dob;
            string Email = string.Empty;
            string Gender = string.Empty;
            do
            {

                Console.WriteLine("Enter User Firstname >");
                firstName = Console.ReadLine();
                // Check if the input is null or whitespace
                if (String.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("Firstname cannot be empty. Please try again.");
                }



            } while (string.IsNullOrWhiteSpace(firstName));



            do
            {

                Console.WriteLine("Enter User lastName >");
                lastName = Console.ReadLine();
                // Check if the input is null or whitespace
                if (String.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("lastName cannot be empty. Please try again.");
                }



            } while (string.IsNullOrWhiteSpace(lastName));



            do
            {

                Console.WriteLine("Enter User DOB (YYYY-MM-DD) >");
                string rdx_DOB = Console.ReadLine();
                // Check if the input is null or whitespace
                if (String.IsNullOrWhiteSpace(rdx_DOB))
                {
                    Console.WriteLine("DOB cannot be empty. Please try again.");
                }
                else
                {

                    if (DateTime.TryParse(rdx_DOB, out Dob))
                    {

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please enter a valid date (YYYY-MM-DD).");
                    }
                }



            } while (true);


            do
            {

                Console.WriteLine("Enter User Email >");
                string rdx_eMail = Console.ReadLine();
                // Check if the input is null or whitespace
                if (String.IsNullOrWhiteSpace(rdx_eMail))
                {
                    Console.WriteLine("DOB cannot be empty. Please try again.");
                }
                else
                {

                    if (CommonUtils.IsValidEmail(rdx_eMail))
                    {
                        Email = rdx_eMail;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid email format. Please enter again.");
                    }
                }



            } while (string.IsNullOrWhiteSpace(Email));


            do
            {
                Console.WriteLine("Select Gender options below:");
                Console.WriteLine("1. Male >");
                Console.WriteLine("2. Female >");
                Console.WriteLine("3. Other >");
                string? rdx_GENDER_SELECTED_OPTION = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(rdx_GENDER_SELECTED_OPTION))
                {
                    switch (rdx_GENDER_SELECTED_OPTION)
                    {
                        case "1":
                            Gender = "Male";
                            break;
                        case "2":
                            Gender = "Female";
                            break;
                        case "3":
                            Gender = "Other";
                            break;
                    }
                }


            }
            while (string.IsNullOrWhiteSpace(Gender));



            // CREATE NEW USER INSTANCE
            User newUser = new User()
            {
                UserId = Guid.NewGuid(),
                First_Name = firstName,
                Last_Name = lastName,
                Full_Name = lastName + " " + firstName,
                Dob = Dob,
                Email = Email,
                Gender = Gender
            };

            try
            {
                ResponseMessage RESPONSE = await userRepository.ADD_Async(newUser);

                if (RESPONSE != null && RESPONSE.IsSuccess)
                {
                    Console.WriteLine($"Product added successfully. Product ID: {newUser.Email}");
                }
                else
                {
                    Console.WriteLine($"Failed to add new user, try again.");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");

            }
            finally
            {

                await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("1"); // USER


            }



        }

        public static async Task VIEW_USERS_ALL()
        {
            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine("**** AVAILABLE USERS ****");

                List<User> USERS = await userRepository.GET_ALL_Async();

                foreach (User user in USERS)
                {
                    Console.WriteLine($"UserId: {user.UserId}, Full_Name: {user.Full_Name}, DOB: {user.Dob}, Email: {user.Email}, Gender: {user.Gender}");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");

            }
            finally
            {
                await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("1"); // USER
            }
        }

        public static async Task VIEW_USER()
        {
            try
            {
                // DISPLAY MESSAGE
                Console.WriteLine($"{Environment.NewLine}**** CHECK USER ****");
                Console.Write("Please enter User Id:");
                Guid UserId;
                do
                {

                    Console.WriteLine("Enter user Id>");
                    // Check if the input is null or whitespace
                    string rdx_UserId = Console.ReadLine();


                    if (!Guid.TryParse(rdx_UserId, out UserId))
                    {
                        Console.WriteLine("Invalid user id. Please enter a valid positive numeric value");
                    }
                    else if (string.IsNullOrWhiteSpace(rdx_UserId))
                    {
                        Console.WriteLine("User Id cannot be empty. Please try again.");
                    }



                } while (UserId == Guid.Empty);

                User user = await userRepository.GET_BY_ID_Async(UserId);
                if (user == null)
                {
                    Console.WriteLine("Can't find existed Product");

                }
                else
                {
                    Console.WriteLine($"UserId: {user.UserId}, Full_Name: {user.Full_Name}, DOB: {user.Dob}, Email: {user.Email}, Gender: {user.Gender}");

                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
            finally
            {
                await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("1"); // USER
            }
        }


        public static async Task UPDATE_USER()
        {
            try
            {
                // DISPLAY AVAILABLE USERS | IF ANY
                Console.WriteLine("**** AVAILABLE USERS ****");

                List<User> USERS = await userRepository.GET_ALL_Async();

                foreach (User user in USERS)
                {
                    Console.WriteLine($"UserId: {user.UserId}, Full_Name: {user.Full_Name}, DOB: {user.Dob}, Email: {user.Email}, Gender: {user.Gender}");
                }

                Console.WriteLine($"{Environment.NewLine}Enter USER ID in order to update");

                Guid userId;
                do
                {

                    Console.WriteLine("Enter User Id>");
                    // Check if the input is null or whitespace
                    string rdx_UserId = Console.ReadLine();


                    if (!Guid.TryParse(rdx_UserId, out userId))
                    {
                        Console.WriteLine("Invalid user Id");
                    }
                    else if (string.IsNullOrWhiteSpace(rdx_UserId))
                    {
                        Console.WriteLine("User Id cannot be empty. Please try again.");
                    }



                } while (userId == Guid.Empty);

                User existingUser = await userRepository.GET_BY_ID_Async(userId);
                if (existingUser == null)
                {
                    Console.WriteLine("Can't find existed User");

                }
                else
                {


                    string modified_FirstName = string.Empty;
                    string modified_LastName = string.Empty;
                    DateTime modified_DOB;
                    string modified_Email = string.Empty;
                    string modified_Gender = string.Empty;

                    Console.WriteLine("Enter update FirstName >");
                    modified_FirstName = Console.ReadLine();
                    // Check if the input is null or whitespace
                    if (String.IsNullOrWhiteSpace(modified_FirstName))
                    {
                        modified_FirstName = existingUser.First_Name;
                    }


                    Console.WriteLine("Enter update LastName >");
                    modified_LastName = Console.ReadLine();
                    // Check if the input is null or whitespace
                    if (String.IsNullOrWhiteSpace(modified_LastName))
                    {
                        modified_LastName = existingUser.Last_Name;
                    }


                    do
                    {
                        Console.WriteLine("Enter User DOB (yyyy-MM-dd):");
                        string rdx_DOB = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(rdx_DOB))
                        {
                            modified_DOB = existingUser.Dob;
                            break;

                        }
                        if (DateTime.TryParse(rdx_DOB, out modified_DOB))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Please enter a valid date (yyyy-MM-dd).");
                        }

                    } while (true);


                    do
                    {
                        Console.WriteLine("Enter User email :");
                        string rdx_email = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(rdx_email))
                        {
                            modified_Email = existingUser.Email;
                            break;
                        }


                        else if (!String.IsNullOrWhiteSpace(rdx_email) && CommonUtils.IsValidEmail(rdx_email))
                        {
                            modified_Email = rdx_email;
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid Email format.");
                        }


                    } while (true);


                    do
                    {
                        Console.WriteLine("Enter User Gender :");
                        string rdx_gender = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(rdx_gender))
                        {
                            modified_Gender = existingUser.Gender;
                            break;
                        }


                        else if (!String.IsNullOrWhiteSpace(rdx_gender) && Array.IndexOf(ConstSystem.userGenderArray, rdx_gender) >= 0)
                        {
                            modified_Gender = rdx_gender;
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid Gender format.");
                        }


                    } while (true);


                    User updateUser = new User()
                    {
                        UserId = existingUser.UserId,
                        First_Name = modified_FirstName,
                        Last_Name = modified_LastName,
                        Full_Name = modified_LastName + " " + modified_FirstName,
                        Dob = modified_DOB,
                        Email = modified_Email,
                        Gender = modified_Gender

                    };
                    ResponseMessage response = await userRepository.UPDATE_Async(existingUser.UserId, updateUser);
                    if (response != null && response.IsSuccess)
                    {
                        Console.WriteLine($"User updated successfully. User ID: {existingUser.Email}");

                    }
                    else
                    {
                        Console.WriteLine($"User updated failed");

                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
            finally
            {

                await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("1"); // USER

            }
        }


        public static async Task DELETE_USER()
        {
            try
            {
                // DISPLAY AVAILABLE USERS | IF ANY
                Console.WriteLine("**** AVAILABLE USERS ****");

                List<User> USERS = await userRepository.GET_ALL_Async();

                foreach (User user in USERS)
                {
                    Console.WriteLine($"UserId: {user.UserId}, Full_Name: {user.Full_Name}, DOB: {user.Dob}, Email: {user.Email}, Gender: {user.Gender}");
                }



                Console.WriteLine($"{Environment.NewLine}Enter USER ID in order to update");

                Guid userId;
                do
                {

                    Console.WriteLine("Enter User Id>");
                    // Check if the input is null or whitespace
                    string rdx_UserId = Console.ReadLine();


                    if (!Guid.TryParse(rdx_UserId, out userId))
                    {
                        Console.WriteLine("Invalid user Id");
                    }
                    else if (string.IsNullOrWhiteSpace(rdx_UserId))
                    {
                        Console.WriteLine("User Id cannot be empty. Please try again.");
                    }



                } while (userId == Guid.Empty);

                User existingUser = await userRepository.GET_BY_ID_Async(userId);
                if (existingUser == null)
                {
                    Console.WriteLine("Can't find existed User");

                }
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Please verify to delete User where Id = {existingUser.UserId} >");
                    Console.WriteLine("1. Yes >");
                    Console.WriteLine("2. No >");
                    Console.WriteLine("0. Exit >");

                    // RECEIVE USER INPUT
                    string? rdx_delete_options;
                    int int_rdx_delete_options;

                    do
                    {
                        rdx_delete_options = Console.ReadLine();

                        if (String.IsNullOrWhiteSpace(rdx_delete_options))
                        {

                            Console.WriteLine("select User option cannot be empty. Please try again.");
                        }
                        else if (!Int32.TryParse(rdx_delete_options, out int_rdx_delete_options))
                        {
                            Console.WriteLine("Must select number option from 0 to 2");
                        }

                        else
                        {
                            switch (int_rdx_delete_options)
                            {
                                case 0:
                                    await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("1"); // USER
                                    break;
                                case 1:
                                    ResponseMessage response = await userRepository.DELETE_Async(existingUser.UserId);
                                    if (response != null && response.IsSuccess)
                                    {
                                        Console.WriteLine("Delete User Successfully !");

                                    }
                                    else
                                    {
                                        Console.WriteLine("Delete User fail !");

                                    }
                                    await HomeController.REDIRECT_TO_SELECTED_OPTION_ADMIN("1"); // USER
                                    break;
                                default:
                                    Console.WriteLine("Invalid option. Please select 0 to 2");
                                    break;

                            }


                        }


                    }
                    while (rdx_delete_options != null && String.IsNullOrWhiteSpace(rdx_delete_options));


                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }

        }

        public static Guid SIGN_OUT_USER()
        {
            ResponseMessage responseMessage = userRepository.LOGOUT_USER();
            return responseMessage.LogInUserId;

        }

    }
}
