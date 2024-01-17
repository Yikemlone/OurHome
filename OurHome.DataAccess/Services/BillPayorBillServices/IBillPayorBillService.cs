using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillPayorBillServices
{
    public interface IBillPayorBillService : IRepositoryService<BillPayorBill>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="billPayorUsers"></param>
        /// <param name="bill"></param>
        /// <param name="billCoOwners"></param>
        /// <returns></returns>
        Task<List<BillPayorBill>> AddAsync(List<User> billPayorUsers, Bill bill, List<User>? billCoOwners = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<BillPayorBill>> GetPayorsBillsAsync(Guid userID);
    }
}
