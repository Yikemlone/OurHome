using OurHome.Models.Models;

namespace OurHome.Server.Services.Bills
{
    public interface IBillsService
    {
        Task<List<Bill>> GetBills();
        Task<bool> CreateBill(Bill bill);
        Task<bool> UpdateBill(Bill bill);
        Task<bool> DeleteBill(Bill bill);

        // Helper Functions
        Task<decimal> DivideBillPayersPrice(decimal billPrice);
        Task<decimal> DivideBillOwnersPrice(decimal billPrice);
        Task CreateNewReocurringBill(Bill bill);
    }
}
