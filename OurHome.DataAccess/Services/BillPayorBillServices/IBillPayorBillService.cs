using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillPayorBillServices
{
    public interface IBillPayorBillService : IRepositoryService<BillPayorBill>
    {
        /// <summary>
        /// Add a list of users to add to a bill that the users need to pay for. Adds the bill co-owners if any are given.
        /// </summary>
        /// <param name="billPayorUsers"></param>
        /// <param name="bill"></param>
        /// <param name="billCoOwners"></param>
        /// <returns>List of bill payor bills</returns>
        Task<List<BillPayorBill>> AddAsync(List<User> billPayorUsers, Bill bill, List<User>? billCoOwners = null);

        /// <summary>
        /// Get all bills that a user needs to pay for
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List of bill payor bills</returns>
        Task<List<BillPayorBill>> GetPayorsBillsAsync(Guid userID);
    }
}
