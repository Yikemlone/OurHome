using OurHome.Shared;

namespace OurHome.Server.Services.Bills
{
    public interface IBillsService
    {
        public BillsDto GetBills();
        public Task UpdateBills(BillsDto updatedBills);
    }
}
