using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using OurHome.DataAccess.Context;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.UserServices
{
    public class UserService : RepositoryService<User>, IUserService
    {
        public UserService(OurHomeDbContext context) : base(context)
        {
        }
    }
}
