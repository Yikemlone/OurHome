using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.HomeServices
{
    public class HomeService : RepositoryService<Home>, IHomeService
    {
        public HomeService(OurHomeDbContext context) : base(context)
        {
        }
    }
}
