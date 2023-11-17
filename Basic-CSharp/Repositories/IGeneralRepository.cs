using Basic_CSharp.Models;

namespace Basic_CSharp.Repositories
{
    public interface IGeneralRepository<T>
    {
        public string ConnectionString { get; set; }
        Task<List<T>> GET_ALL_Async();
        Task<T> GET_BY_ID_Async(Guid Id);
        Task<ResponseMessage> ADD_Async(T entity);
        Task<ResponseMessage> UPDATE_Async(Guid Id, T entity);
        Task<ResponseMessage> DELETE_Async(Guid Id);
        int CHECK_EXIST(Guid Id);

    }
}
