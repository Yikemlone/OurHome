using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillPayorBillServices
{
    public interface IBillPayorBillService : IRepositoryService<BillPayorBill>
    {
        Task<List<BillPayorBill>> AddAsync(List<User> billPayorUsers, Bill bill, List<User> billCoOwners = null);
        Task<List<BillPayorBill>> GetPayorsBillsAsync(Guid userID);
    }
}
