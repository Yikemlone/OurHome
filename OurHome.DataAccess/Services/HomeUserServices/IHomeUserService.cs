using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;

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
    }
}
