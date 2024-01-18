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
                HomeID = bill.HomeID,
                Price = bill.Price,
                Reoccurring = bill.Reoccurring,
                SplitBill = bill.SplitBill,
                Note = bill.Note,
                DateTime = bill.DateTime.AddMonths(1),
                CoOwners = bill.CoOwners
            };

            // Do I want to add this to the db here or just retu

            return newBill;
        }
    }
}
