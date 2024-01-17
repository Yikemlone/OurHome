using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillCoOwnerServices
{
    public interface IBillCoOwnerService : IRepositoryService<BillCoOwner>
    {
        /// <summary>
        /// Adds a list of Co-owners to a bill
        /// </summary>
        /// <param name="coOwners"></param>
        /// <param name="bill"></param>
        /// <returns>List of BillCoOwner</returns>
        Task<List<BillCoOwner>> AddAsync(List<User> coOwners, Bill bill);
    }
}

