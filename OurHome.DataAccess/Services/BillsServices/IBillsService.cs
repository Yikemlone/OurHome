using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.Server.Services.BillsServices
{
    public interface IBillsService : IRepositoryService<Bill>
    {
        Task<List<Bill>>GetAllAsync(Guid billOwnerID);
        Bill CreateNewReocurringBill(Bill bill);
    }
}
    