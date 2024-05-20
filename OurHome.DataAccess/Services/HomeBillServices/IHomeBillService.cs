using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.HomeBillServices
{
    public interface IHomeBillService : IRepositoryService<HomeBill>
    {
        /// <summary>
        /// Adds a list of HomeBills to the database
        /// </summary>
        /// <param name="homeBills"></param>
        /// <returns></returns>
        Task AddAsync(List<HomeBill> homeBills);

        /// <summary>
        /// Gets a list of HomeBills by HomeID
        /// </summary>
        /// <param name="homeID"></param>
        /// <returns></returns>
        Task<List<HomeBill>> GetHomeBillsByHomeIDAsync(int homeID);
    }
}
