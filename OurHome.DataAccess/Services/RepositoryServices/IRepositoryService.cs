namespace OurHome.DataAccess.Services.RepositoryServices
{
    public interface IRepositoryService<T> where T : class
    {
        /// <summary>
        /// Adds an object to the database of type T
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Async operation</returns>
        Task AddAsync(T obj);

        /// <summary>
        /// Updates an object in the database of type T
        /// </summary>
        /// <param name="obj"></param>
        void Update(T obj);

        /// <summary>
        /// Deletes an object from the database of type T
        /// </summary>
        /// <param name="obj"></param>
        void Delete(T obj);

        /// <summary>
        /// Gets all objects from the database of type T
        /// </summary>
        /// <returns></returns>
        Task< List<T>>GetAllAsync();

        /// <summary>
        /// Gets an object from the database of type T by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);
    }
}
