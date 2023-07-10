using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
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


    }
}
