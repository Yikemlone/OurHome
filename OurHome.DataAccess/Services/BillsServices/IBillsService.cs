using OurHome.DataAccess.Services.RepoService;
using OurHome.Models.Models;

namespace OurHome.Server.Services.Bills
{
    public interface IBillsService : IRepositoryService<Bill>
    {
        Task<decimal> DivideBillPayersPrice(decimal billPrice);
        Task<decimal> DivideBillOwnersPrice(decimal billPrice);
        Task CreateNewReocurringBill(Bill bill);
    }
}
