using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
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

        public async Task<List<BillPayorBill>> AddAsync(List<User> billPayorUsers, Bill bill, List<User>? billCoOwners = null)
        {
            List<BillPayorBill> billPayorBills = new List<BillPayorBill>();
            int coOwnersCount = billCoOwners?.Count ?? 0;
            decimal? userPrice;


            // Add the bill owner to the list of payors
            foreach (var billPayor in billPayorUsers) 
            {
                // If there are co-owners, add them to the list of payees, so to create a bill for each co-owner
                if (billCoOwners != null && coOwnersCount > 0)
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
                        Payee = bill.BillOwner,
                        Payor = billPayor
                    });
                }
            }

            // If the bill price is split and there are co-owners, split the user bill price between the co-owners and the payor
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

            // Set the user price for each bill payor bill
            foreach (var billPayor in billPayorBills)
            {
                billPayor.UserPrice = userPrice;
            }

            await _context.BillPayors.AddRangeAsync(billPayorBills);

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
