using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.UserBillServices
{
    public interface IBillPayorService
    {
        Task<BillPayorBill> AddAllBillPayorAsync(List<User> billPayors, Bill bill);
        Task<BillPayorBill> GetBillPayorBillAsync(int id);

        Task<BillPayorBill> UpdateBillPayorBill();
    }
}
