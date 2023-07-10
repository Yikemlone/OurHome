using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;
using OurHome.Server.Services.Bills;

namespace OurHome.DataAccess.Services.BillsServices
{
    public class BillService : IRepositoryService<Bill>, IBillsService
    {
        private readonly OurHomeDbContext _context;

        public BillService(OurHomeDbContext context)
        {
            _context = context;
        }

        public async Task<Bill> AddAsync(Bill obj)
        {
            throw new NotImplementedException();
        }

        public async Task<Bill> UpdateAsync(Bill obj)
        {
            throw new NotImplementedException();
        }

        public async void DeleteAsync(Bill obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bill>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Bill> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Bill> CreateNewReocurringBill(Bill bill)
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


        public async Task<decimal?> CalculateBillPrices(Bill bill) 
        {
            // A few things can happen here:
            // If no split, but still co-owner, we should only divide co-owner only
            // If split and co-owner, we should divide by co-owners, then payors
            // If split and no co-owner, we should only divide by payors

            var billPrice = bill.Price;

            if (bill.BillCoOwners.Count > 0)
            {
                int billOwnersCount = bill.BillCoOwners.Count + 1; // Plus 1 to account for the bill owner
                billPrice = bill.Price / billOwnersCount;
            }

            return billPrice;

            //if (bill.SplitBill) 
            //{
            //    billPayorPrice = billPrice / billPayors.Count;
            //}
        }

    }
}
