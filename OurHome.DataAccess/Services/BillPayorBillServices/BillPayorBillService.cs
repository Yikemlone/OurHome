using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.Model.Models;
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

        public async Task<List<BillPayorBill>> AddAsync(List<User> billPayorUsers, Bill bill, List<User> billCoOwners = null)
        {
            List<BillPayorBill> billPayorBills = new List<BillPayorBill>();
            int coOwnersCount = billCoOwners?.Count ?? 0;
            decimal? userPrice;

            foreach (var billPayor in billPayorUsers) 
            {
                if (coOwnersCount > 0)
                {
                    foreach (var coOwner in billCoOwners)
                    {
                        billPayorBills.Add(new() 
                        {
                            Bill = bill,
                            Payor = billPayor,
                            Payee = coOwner
                        });
                    }
                }
                else    
                {
                    billPayorBills.Add(new()
                    {
                        Bill = bill,
                        Payor = billPayor,
                        Payee = bill.BillOwner
                    });
                }
            }


            if (bill.SplitBill && coOwnersCount > 0)
            {
                userPrice = bill.Price / (coOwnersCount * billPayorUsers.Count);
            }
            else if (coOwnersCount > 0)
            {
                userPrice = bill.Price / coOwnersCount;
            }
            else if (bill.SplitBill) 
            {
                userPrice = bill.Price / billPayorUsers.Count;
            }
            else
            {
                userPrice = bill.Price;
            }


            foreach (var billPayor in billPayorBills)
            {
                billPayor.UserPrice = userPrice;
            }

            await _context.AddRangeAsync(billPayorBills);

            return billPayorBills;
        }

        public async Task<List<BillPayorBill>> GetPayorsBillsAsync(Guid userID)
        {
            var billPayorBills = await _context.BillPayors
                .Where(u => u.PayorID == userID)
                .Select(e => e)
                .ToListAsync();

            return billPayorBills;
        }
    }
}
