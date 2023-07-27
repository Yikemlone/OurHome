using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.Server.Services.BillServices
{
    public interface IBillService : IRepositoryService<Bill>
    {
        Task<List<Bill>>GetAllAsync(User billOwner);
        Bill CreateNewReocurringBill(Bill bill);
    }
}
    