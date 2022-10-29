using OurHome.Shared.DTO;

namespace OurHome.Server.Services.Bills
{
    public interface IBillsService
    {
        public Task<IEnumerable<BillDto>> GetBills();
        public Task UpdateBills(BillDto updatedBills);
        public Task<PersonsBillsDto> GetPersonsBills(int personID);
        public Task<IEnumerable<PersonsBillsDto>> GetPeoplesBills();
        public Task<IEnumerable<PastBillDto>> GetPastBills();
        public Task<IEnumerable<BillDueDateDto>> GetBillDueDates();
        public Task<IEnumerable<PayedBillDto>> GetPayedBills();
    }
}
