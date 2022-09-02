using OurHome.Shared.DTO;

namespace OurHome.Server.Services.Bills
{
    public class BillsService : IBillsService
    {
        public Task<IEnumerable<BillsDto>> GetBills()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonsBillsDto>> GetPeoplesBills()
        {
            throw new NotImplementedException();
        }

        public Task<PersonsBillsDto> GetPersonsBills(string person)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBills(BillsDto updatedBills)
        {
            throw new NotImplementedException();
        }
    }
}
