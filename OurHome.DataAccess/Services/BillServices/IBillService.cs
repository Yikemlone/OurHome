using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.Server.Services.BillServices
{
    public interface IBillService : IRepositoryService<Bill>
    {
        /// <summary>
        /// Gets a list of all bills a user owns
        /// </summary>
        /// <param name="billOwner"></param>
        /// <returns>List of Bills</returns>
        Task<List<Bill>>GetAllAsync(User billOwner);
    
        /// <summary>
        /// Creates a new reocurring bill based on the given bill
        /// </summary>
        /// <param name="bill"></param>
        /// <returns>New Bill with updated dates</returns>
        Bill CreateNewReocurringBill(Bill bill);

        /// <summary>
        /// Will return a list of all bills that a user has in a specific home
        /// </summary>
        /// <param name="user"></param>
        /// <param name="homeID"></param>
        /// <returns></returns>
        Task<List<Bill>> GetUserBillsByHomeIDAsync(User user, int homeID);

    }
}