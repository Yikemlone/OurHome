using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;

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
    }
}
