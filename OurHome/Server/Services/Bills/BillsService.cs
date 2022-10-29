using OurHome.Server.Models;
using OurHome.Shared.DTO;
using System;
using System.Collections.Generic;

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

        public async Task<IEnumerable<BillDto>> GetBills()
        {
            IEnumerable<BillDto> billsList = _context.Bills
                .Select(c => new BillDto()
                {
                    BillID = c.BillID,
                    Bill = c.Bill,
                    Price = c.Price
                });

            return billsList;
        }

        public async Task<IEnumerable<PersonsBillsDto>> GetPeoplesBills()
        {
            IEnumerable<PersonsBillsDto> billsList = _context.PersonsBills
                .Join(_context.Person,
                    bills => bills.PersonID,
                    person => person.PersonID,
                    (bills, person) => new PersonsBillsDto()
                    {
                        Name = person.PersonName.ToString(), // NEED TO UPDATE DATABASE FOR THIS
                        Bins = bills.Bins,
                        Electricity = bills.Electricity,
                        Internet = bills.Internet,
                        Oil = bills.Oil,
                        Rent = bills.Rent 
                    });

            return billsList;
        }

        public async Task<PersonsBillsDto> GetPersonsBills(int personID)
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

            return personsBills;
        }

        // TEST IF THIS EVEN WORKS
        public async Task UpdateBills(BillDto updatedBills)
        {
            var oldBills = (BillDto) _context.Bills
            .Where(c => updatedBills.BillID == c.BillID);

            oldBills.Bill = updatedBills.Bill;
            oldBills.Price = updatedBills.Price;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<PastBillDto>> GetPastBills()
        {
            IEnumerable<PastBillDto> pastBills = _context.PastBills
                .Select(pb => new PastBillDto
                {
                    PastBillID = pb.PastBillID,
                    BillMonth = pb.BillMonth,
                    Bins = pb.Bins,
                    Electric = pb.Electric, 
                    Internet = pb.Internet,
                    Rent = pb.Rent,
                    Oil = pb.Oil
                });

            return pastBills;
        }

        public async Task<IEnumerable<PayedBillDto>> GetPayedBills()
        {
            IEnumerable<PayedBillDto> payedBills = _context.PayedBills
                .Select(payedBills => new PayedBillDto
                {
                    PayedBillID = payedBills.PayedBillID,
                    PaymentType = payedBills.PaymentType,
                    BillDate = payedBills.BillDate,
                    Bill = payedBills.Bill,
                    PersonID = payedBills.PersonID
                });

            return payedBills;
        }

        public async Task<IEnumerable<BillDueDateDto>> GetBillDueDates()
        {
            IEnumerable<BillDueDateDto> payedBills = _context.BillsDueDate
             .Select(due => new BillDueDateDto
             {
                 BillID = due.BillID,
                 BillDueDate = due.BillDueDate
             });

            return payedBills;
        }

    }
}
