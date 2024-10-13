using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.HomeServices
{
    public class HomeService : RepositoryService<Home>, IHomeService
    {
        private readonly OurHomeDbContext _context;

        public HomeService(OurHomeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Home>> GetAllAsync(User user)
        {
            var userHomes = await _context.Homes
                .Where(h => h.HomeOwnerID == user.Id)
                .Select(m => m)
                .ToListAsync();

            return userHomes;
        }
    }
}
