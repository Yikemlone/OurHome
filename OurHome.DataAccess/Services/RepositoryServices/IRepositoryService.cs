namespace OurHome.DataAccess.Services.RepositoryServices
{
    public interface IRepositoryService<T> where T : class
    {
        /// <summary>
        /// Adds the T object to the database context.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Return the T object that was added.</returns>
        abstract Task<T> AddAsync(T obj);

        /// <summary> 
        /// Updates the T object to the database context.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Returns the T object that was updated</returns>
        Task<T> UpdateAsync(T obj);

        /// <summary>
        /// Deletes the T object from the database.
        /// </summary>
        /// <param name="obj"></param>
        void DeleteAsync(T obj);

        /// <summary>
        /// Get all of the T objects
        /// </summary>
        /// <returns>Returns an IEnumerable of T</returns>
        Task<IEnumerable<T>>GetAllAsync();

        /// <summary>
        /// Gets the T object of the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the T object witht the ID value passed in.</returns>
        Task<T> GetAsync(int id);
    }
}
