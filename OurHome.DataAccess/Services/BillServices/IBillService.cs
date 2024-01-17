using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.Server.Services.BillServices
{
    public interface IBillService : IRepositoryService<Bill>
    {
        /// <summary>
        /// Gets a list of all bills for a given user 
        /// </summary>
        /// <param name="billOwner"></param>
        /// <returns>List of Bills</returns>
        public Task<List<Bill>>GetAllAsync(User billOwner);
    
        /// <summary>
        /// Creates a new reocurring bill based on the given bill
        /// </summary>
        /// <param name="bill"></param>
        /// <returns>New Bill with updated dates</returns>
        Bill CreateNewReocurringBill(Bill bill);
    }
}
    