using OurHome.Server.Models;
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

        public Task<IEnumerable<BillDto>> GetBills()
        {
            IEnumerable<BillDto> billsList = _context.Bills
                .Select(c => new BillDto()
                {
                    BillID = c.BillID,
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
                    PersonID = c.PersonID,
                    Bins = c.Bins,
                    Electricity = c.Electricity,
                    Internet = c.Internet,
                    Oil = c.Oil,
                    Rent = c.Rent 
                }).ToList();

            return Task.FromResult(billsList);
        }

        public Task<PersonsBillsDto> GetPersonsBills(int personID)
        {
            PersonsBillsDto personsBills = (PersonsBillsDto) _context.PersonsBills
                .Where(c => personID == c.PersonID)
                .Select(c => new PersonsBillsDto()
                {
                    PersonID = c.PersonID,
                    Bins = c.Bins,
                    Electricity = c.Electricity,
                    Internet = c.Internet,
                    Oil = c.Oil,
                    Rent = c.Rent
                });

            return Task.FromResult(personsBills);
        }

        public Task UpdateBills(BillDto updatedBills)
        {
            var oldBills = (BillDto) _context.Bills
            .Where(c => updatedBills.BillID == c.BillID);

            oldBills.Bill = updatedBills.Bill;
            oldBills.Price = updatedBills.Price;

            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
