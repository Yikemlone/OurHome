using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.Models.Models;
using OurHome.Server.Services.BillServices;

namespace OurHome.DataAccess.Services.BillServices
{
    public class BillService : RepositoryService<Bill>, IBillService
    {
        private readonly OurHomeDbContext _context;

        public BillService(OurHomeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Bill>> GetAllAsync(User billOwner)
        {
            List<Bill> bills = await _context.Bills.AsNoTracking()
                .Where(u => u.BillOwnerID == billOwner.Id)
                .Select(m => m)
                .ToListAsync();

            return bills;
        }

        public Bill CreateNewReocurringBill(Bill bill)
        {
            Bill newBill = new Bill()
            {
                BillName = bill.BillName,
                BillOwnerID = bill.BillOwnerID,
                BillOwner = bill.BillOwner,
                HomeID = bill.HomeID,
                Home = bill.Home,
                Price = bill.Price,
                Reoccurring = bill.Reoccurring,
                SplitBill = bill.SplitBill,
                Note = bill.Note,
                DateTime = bill.DateTime.AddMonths(1),
                CoOwners = bill.CoOwners
            };

            // Do I want to add this to the db here or just return it?

            return newBill;
        }

        public override void Delete(Bill bill)
        {
            // I want to check if a bill has any payments before deleting it
            // If it does, I don't want to delete it

            var payments = _context.BillPayors
                .Where(p => p.BillID == bill.ID)
                .ToList();

            foreach(var payment in payments)
            {
                if(payment.Payed) return;
            }

            _context.Bills.Remove(bill);
        }

        public async Task<List<Bill>> GetUserBillsByHomeIDAsync(User user, int homeID)
        {
            var userHomeBills = await _context.Bills
                .Where(u => u.BillOwnerID == user.Id && u.HomeID == homeID)
                .ToListAsync();

            return userHomeBills;
        }
    }
}
