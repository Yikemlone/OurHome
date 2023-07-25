namespace OurHome.DataAccess.Services.RepositoryServices
{
    public interface IRepositoryService<T> where T : class
    {
        Task AddAsync(T obj);
        void Update(T obj);
        void Delete(T obj);
        Task< List<T>>GetAllAsync();
        Task<T> GetAsync(int id);
    }
}
