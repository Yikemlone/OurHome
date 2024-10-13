using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.HomeServices
{
    public interface IHomeService : IRepositoryService<Home>
    {
        /// <summary>
        /// Returns a list of all the homes a users owns
        /// </summary>
        /// <param name="user"></param>
        /// <returns>List of homes</returns>
        Task<List<Home>> GetAllAsync(User user);
    }
}
