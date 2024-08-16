using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.BillCoOwnerServices;
using OurHome.DataAccess.Services.BillPayorBillServices;
using OurHome.DataAccess.Services.BillServices;
using OurHome.DataAccess.Services.HomeBillServices;
using OurHome.DataAccess.Services.HomeServices;
using OurHome.DataAccess.Services.HomeUserServices;
using OurHome.DataAccess.Services.InvitationServices;
using OurHome.DataAccess.Services.UserServices;

namespace OurHome.DataAccess.Services.UnitOfWorkServices
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        public IBillService BillService { get; private set; }
        public IBillPayorBillService BillPayorBillService { get; private set; }
        public IBillCoOwnerService BillCoOwnerService { get; private set; }
        public IHomeService HomeService { get; private set; }
        public IHomeBillService HomeBillService { get; private set; }
        public IHomeUserService HomeUserService { get; private set; }
        public IInvitationService InvitationService { get; private set; }
        public IUserService UserService { get; private set; }

        private readonly OurHomeDbContext _context;

        public UnitOfWorkService(OurHomeDbContext context)
        {
            _context = context;
            BillService = new BillService(_context);
            BillPayorBillService = new BillPayorBillService(_context);
            BillCoOwnerService = new BillCoOwnerService(_context);
            HomeService = new HomeService(_context);
            HomeBillService = new HomeBillService(_context);
            HomeUserService = new HomeUserService(_context);
            InvitationService = new InvitationService(_context);
            UserService = new UserService(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
    