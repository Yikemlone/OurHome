using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillPayorBillServices
{
    public class BillPayorBillService : RepositoryService<BillPayorBill>, IBillPayorBillService
    {
        private readonly OurHomeDbContext _context;

        public BillPayorBillService(OurHomeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BillPayorBill>> AddAsync(List<User> billPayorUsers, Bill bill)
        {
            List<BillPayorBill> billPayorBills = new List<BillPayorBill>();

            foreach (var billPayor in billPayorUsers) 
            {
                billPayorBills.Add(new BillPayorBill
                {
                    Bill = bill,
                    User = billPayor
                });
            }

            await _context.AddRangeAsync(billPayorBills);

            return billPayorBills;
        }

        public async Task<List<BillPayorBill>> GetPayorsBillsAsync(Guid userID)
        {
            var billPayorBills = await _context.BillPayors
                .Where(u => u.UserID == userID)
                .Select(e => e)
                .ToListAsync();

            return billPayorBills;
        }
    }
}
