using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.HomeUserServices
{
    public interface IHomeUserService : IRepositoryService<HomeUser>
    {
        /// <summary>
        /// Creates an invitation to a home
        /// </summary>
        /// <param name="invitation"></param>
        /// <returns></returns>
        Task AddAsync(Invitation invitation);

        /// <summary>
        /// Gets a list of users in a home by HomeID
        /// </summary>
        /// <param name="homeID"></param>
        /// <returns>List home users</returns>
        Task<List<HomeUser>> GetHomeUsersByHomeIDAsync(int homeID);

        /// <summary>
        /// Adds the home owner to the home as a HomeUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="home"></param>
        /// <returns></returns>
        Task AddHomeOwnerAsync(User user, Home home);
    }
}
