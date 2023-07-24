using OurHome.Models.Models;

namespace OurHome.Server.Services.Bills
{
    public interface IBillsService
    {
        Task<Bill> AddBillAsync(Bill bill);
        Task<Bill?> GetBillAsync(int ID);
        Task<List<Bill>>GetAllBillsAsync(Guid billOwnerID);
        Bill CreateNewReocurringBill(Bill bill);
    }
}
    