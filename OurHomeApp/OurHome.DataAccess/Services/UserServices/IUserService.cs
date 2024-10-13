using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.UserServices
{
    public interface IUserService : IRepositoryService<User>
    {
        /// <summary>
        /// Get a user by their Guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns>The users details</returns>
        Task<User> GetAsync(Guid userGuid);
    }
}
