using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using OurHome.DataAccess.Context;
using OurHome.Model.Models;

namespace OurHome.DataAccess.Services.HomeServices
{
    public class HomeService : RepositoryService<Home>, IHomeService
    {
        public HomeService(OurHomeDbContext context) : base(context)
        {
        }
    }
}
