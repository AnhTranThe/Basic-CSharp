using Basic_CSharp.Models;

namespace Basic_CSharp.Repositories
{
    public interface IUserRepository : IGeneralRepository<User>
    {

        ResponseMessage LOGIN_USER();

        ResponseMessage LOGOUT_USER();

    }
}
