using OurHome.DataAccess.Context;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillCoOwnerService
{
    public class BillCoOwnerService : IBillCoOwnerService
    {
        private readonly OurHomeDbContext _context;

        public BillCoOwnerService(OurHomeDbContext context)
        {
            _context = context;
        }

        public async Task<List<BillCoOwner>> AddAllAsync(List<User> coOwners, Bill bill)
        {
            List<BillCoOwner> billCoOwners = new List<BillCoOwner>();

            foreach (var coOwner in coOwners)
            {
                billCoOwners.Add(new()
                {
                    Bill = bill,
                    Price = bill.Price/coOwners.Count,
                    User = coOwner
                });
            }

            await _context.BillCoOwners.AddRangeAsync(billCoOwners);

            return billCoOwners;
        }
    }
}
