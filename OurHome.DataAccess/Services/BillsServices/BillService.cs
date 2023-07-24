using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.Models.Models;
using OurHome.Server.Services.Bills;

namespace OurHome.DataAccess.Services.BillsServices
{
    public class BillService : IBillsService
    {
        private readonly OurHomeDbContext _context;

        public BillService(OurHomeDbContext context)
        {
            _context = context;
        }

        public async Task<Bill> AddBillAsync(Bill obj)
        {
            Bill newBill = new()
            {
                BillName = obj.BillName,
                BillOwner = obj.BillOwner,
                DateTime = obj.DateTime,
                HomeID = obj.HomeID,
                Price = obj.Price,
                Note = obj.Note,
                Reoccurring = obj.Reoccurring,
                SplitBill = obj.SplitBill
            };

            await _context.Bills.AddAsync(newBill);

            return newBill;
        }

        public async Task<List<Bill>> GetAllBillsAsync(Guid billOwnerID)
        {
            List<Bill> bills = await _context.Bills
                .Where(u => u.BillOwnerID == billOwnerID)
                .Select(m => m)
                .ToListAsync();

            return bills;
        }

        public async Task<Bill?> GetBillAsync(int id)
        {
            Bill? bills = await _context.Bills
                .Where(b => b.ID == id)
                .Select(m => m).FirstOrDefaultAsync();

            return bills;
        }

        public Bill CreateNewReocurringBill(Bill bill)
        {
            Bill newBill = new Bill()
            {
                BillName = bill.BillName,
                BillOwnerID = bill.BillOwnerID,
                HomeID = bill.HomeID,
                Price = bill.Price,
                Reoccurring = bill.Reoccurring,
                SplitBill = bill.SplitBill,
                Note = bill.Note,
                DateTime = bill.DateTime.AddMonths(1),
                BillCoOwners = bill.BillCoOwners
            };

            return newBill;
        }




        // NEED TO THINK ABOUT THESE MORE
        public async Task<Bill> UpdateAsync(Bill obj)
        {
            // If user needs to update, ensure it's correct user maybe?
            _context.Update(obj);
            return obj;
        }
    }
}
