using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.Server.Services.Bills
{
    public interface IBillsService : IRepositoryService<Bill>
    {
        Task<decimal> DivideBillOwnersPrice(decimal billPrice, int totalOwners);
        Task<Bill> CreateNewReocurringBill(Bill bill);
    }
}
    