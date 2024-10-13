using Microsoft.EntityFrameworkCore;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.HomeUserServices
{
    public class HomeUserService : RepositoryService<HomeUser>, IHomeUserService
    {

        private readonly OurHomeDbContext _context;
        public HomeUserService(OurHomeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(Invitation invitation)
        {
            if (invitation.Status.Equals("ACCEPTED")) 
            {
                await _context.AddAsync(new HomeUser() 
                { 
                    Home = invitation.Home,
                    User = invitation.ToUser
                });
            } 
        }

        public async Task<List<HomeUser>> GetHomeUsersByHomeIDAsync(int homeID)
        {
            var homeUsers = await _context.HomeUsers
                .Where(h => h.HomeID == homeID)
                .Select(m => m)
                .ToListAsync();

            return homeUsers;
        }

        public async Task AddHomeOwnerAsync(User user, Home home)
        {
            if (home.HomeOwnerID == user.Id)
            {
                await _context.AddAsync(new HomeUser()
                {
                    Home = home,
                    User = user
                });
            }
        }

        public async Task<bool> IsUserInHomeAsync(User user, int homeID)
        {
            var homeUser = await _context.HomeUsers
                .Where(h => h.HomeID == homeID && h.UserID == user.Id)
                .Select(m => m)
                .FirstOrDefaultAsync();

            return homeUser != null;
        }   
    }
}
