using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillPayorBillServices
{
    public interface IBillPayorBillService : IRepositoryService<BillPayorBill>
    {
        Task<List<BillPayorBill>> AddAsync(List<User> billPayorUsers, Bill bill);
        Task<List<BillPayorBill>> GetPayorsBillsAsync(Guid userID);
    }
}
