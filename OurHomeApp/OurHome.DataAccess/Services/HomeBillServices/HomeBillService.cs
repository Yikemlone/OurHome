using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.HomeBillServices
{
    public class HomeBillService : RepositoryService<HomeBill>, IHomeBillService
    {
        private readonly OurHomeDbContext _context;

        public HomeBillService(OurHomeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(List<HomeBill> homeBills)
        {
            await _context.AddRangeAsync(homeBills);
        }

        public async Task<List<HomeBill>> GetHomeBillsByHomeIDAsync(int homeID)
        {
            var homeBills = await _context.HomeBills
                .Where(h => h.HomeID == homeID)
                .Select(m => m)
                .ToListAsync();

            return homeBills;
        }
    }
}
