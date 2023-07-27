using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.HomeUsersServices;
using OurHome.Model.Models;

namespace OurHome.DataAccess.Services.HomeUserServices
{
    public class HomeUserService : RepositoryService<HomeUser>, IHomeUserService
    {
        public HomeUserService(OurHomeDbContext context) : base(context)
        {
        }
    }
}
