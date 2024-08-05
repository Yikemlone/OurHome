using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillCoOwnerServices
{
    public interface IBillCoOwnerService : IRepositoryService<BillCoOwner>
    {

        Task AddAsync(List<BillCoOwner> billCoOwners1);

        ///// <summary>
        ///// Adds a list of Co-owners to a bill
        ///// </summary>
        ///// <param name="coOwners"></param>
        ///// <param name="bill"></param>
        ///// <returns>List of BillCoOwner</returns>
        //Task<List<BillCoOwner>> AddAsync(List<User> coOwners, Bill bill);

        /// <summary>
        /// Returns a list of all Co-owners of a bill
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        Task<List<BillCoOwner>> GetAllAsync(int billID);

        /// <summary>
        /// Returns a Co-owner of a bill by compond key
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<BillCoOwner> GetAsync(int billID, Guid userID);
    }
}

