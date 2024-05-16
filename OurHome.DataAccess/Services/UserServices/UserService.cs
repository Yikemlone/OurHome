using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.UserServices
{
    public class UserService : RepositoryService<User>, IUserService
    {
        private readonly OurHomeDbContext _context;

        public UserService(OurHomeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(Guid userGuid)
        {
            var user = await _context.Users
                .Where(u => u.Id == userGuid)
                .Select(m => m)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
