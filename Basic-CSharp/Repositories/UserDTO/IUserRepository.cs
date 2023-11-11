using Basic_CSharp.Models;

namespace Basic_CSharp.Repositories
{
    public interface IUserRepository
    {

        // METHOD IMPLEMENTATIONS
        Task<List<User>> GET_ALL_USERS_Async();

        Task<User> GET_USER_Async();

        Task<ResponseMessage> ADD_USER_Async();

        Task<ResponseMessage> UPDATE_USER_Async();

        Task<ResponseMessage> DELETE_USER_Async();

        Task<ResponseMessage> LOGIN_USER_Async(string loggedInUserId);

        Task<ResponseMessage> LOGOUT_USER_Async(string loggedInUserId);

    }
}
