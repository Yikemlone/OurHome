using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using OurHome.DataAccess.Context;
using OurHome.Model.Models;

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
    }
}
