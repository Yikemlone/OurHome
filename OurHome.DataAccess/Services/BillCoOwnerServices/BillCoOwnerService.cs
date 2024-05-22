using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillCoOwnerServices
{
    public class BillCoOwnerService : RepositoryService<BillCoOwner>, IBillCoOwnerService
    {
        private readonly OurHomeDbContext _context;

        public BillCoOwnerService(OurHomeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(List<BillCoOwner> billCoOwners)
        {
            await _context.BillCoOwners.AddRangeAsync(billCoOwners);
        }

        public async Task<List<BillCoOwner>> GetAllAsync(int billID)
        {
            var billCoOwners = await _context.BillCoOwners
                .Where(b => b.BillID == billID)
                .Select(m => m)
                .ToListAsync();
            
            return billCoOwners;
        }

        public async Task<BillCoOwner> GetAsync(int billID, Guid userID)
        {
            var billCoOwner = await _context.BillCoOwners
                .Where(b => b.BillID == billID && b.UserID == userID)
                .Select(m => m)
                .FirstOrDefaultAsync();

            return billCoOwner;
        }
    }
}
