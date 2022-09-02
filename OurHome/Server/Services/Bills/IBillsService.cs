using OurHome.Shared.DTO;

namespace OurHome.Server.Services.Bills
{
    public interface IBillsService
    {
        public Task<IEnumerable<BillsDto>> GetBills();
        public Task UpdateBills(BillsDto updatedBills);
        public Task<PersonsBillsDto> GetPersonsBills(string person);
        public Task<IEnumerable<PersonsBillsDto>> GetPeoplesBills();

    }
}
