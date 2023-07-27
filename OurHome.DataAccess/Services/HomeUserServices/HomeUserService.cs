using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using OurHome.DataAccess.Context;
using OurHome.Model.Models;

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
    }
}
