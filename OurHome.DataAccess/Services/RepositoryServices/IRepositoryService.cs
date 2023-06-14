namespace OurHome.DataAccess.Services.RepoService
{
    public interface IRepositoryService<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();

        Task<bool> Add(T obj);
        Task<bool> Update(T obj);
        Task<bool> Delete(T obj);
    }
}
