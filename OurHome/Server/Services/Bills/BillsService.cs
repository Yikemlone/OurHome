using OurHome.Shared.DTO;
using System;

namespace OurHome.Server.Services.Bills
{
    public class BillsService : IBillsService
    {
        private readonly OurHomeDbContext _context;

        public BillsService(
            OurHomeDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<BillsDto>> GetBills()
        {
            IEnumerable<BillsDto> billsList = _context.Bills
                .Select(c => new BillsDto()
                {
                    ID = c.ID,
                    Bill = c.Bill,
                    Price = c.Price
                }).ToList();

            return Task.FromResult(billsList);
        }

        public Task<IEnumerable<PersonsBillsDto>> GetPeoplesBills()
        {
            IEnumerable<PersonsBillsDto> billsList = _context.PersonsBills
                .Select(c => new PersonsBillsDto()
                {
                    Bins = c.Bins,
                    Electricity = c.Electricity,
                    Internet = c.Internet,
                    Milk = c.Milk,
                    Name = c.Name,
                    Oil = c.Oil,
                    Rent = c.Rent 
                }).ToList();

            return Task.FromResult(billsList);
        }

        public Task<PersonsBillsDto> GetPersonsBills(string person)
        {
            PersonsBillsDto personsBills = (PersonsBillsDto) _context.PersonsBills
                .Where(c => person == c.Name)
                .Select(c => new PersonsBillsDto()
                {
                    Bins = c.Bins,
                    Electricity = c.Electricity,
                    Internet = c.Internet,
                    Milk = c.Milk,
                    Name = c.Name,
                    Oil = c.Oil,
                    Rent = c.Rent
                });

            return Task.FromResult(personsBills);
        }

        public Task UpdateBills(BillsDto updatedBills)
        {
            var oldBills = (BillsDto) _context.Bills
            .Where(c => updatedBills.ID == c.ID);

            oldBills.Bill = updatedBills.Bill;
            oldBills.Price = updatedBills.Price;

            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
