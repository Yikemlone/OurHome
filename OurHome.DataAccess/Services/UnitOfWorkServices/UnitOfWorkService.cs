using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.BillCoOwnerServices;
using OurHome.DataAccess.Services.BillPayorBillServices;
using OurHome.DataAccess.Services.BillsServices;
using OurHome.DataAccess.Services.HomeBillServices;
using OurHome.DataAccess.Services.HomeServices;
using OurHome.DataAccess.Services.HomeUsersServices;
using OurHome.DataAccess.Services.InvitationServices;
using OurHome.Server.Services.BillsServices;
using System.Runtime.CompilerServices;

namespace OurHome.DataAccess.Services.UnitOfWorkServices
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        public IBillsService BillService { get; private set; }
        public IBillPayorBillService BillPayorBillService { get; private set; }
        public IBillCoOwnerService BillCoOwnerService { get; private set; }
        public IHomeService HomeService { get; private set; }
        public IHomeBillService HomeBillService { get; private set; }
        public IHomeUserService HomeUserService { get; private set; }
        public IInvitationService InvitationService { get; private set; }

        private readonly OurHomeDbContext _context;

        public UnitOfWorkService(OurHomeDbContext context)
        {
            _context = context;
            BillService = new BillService(_context);
            BillPayorBillService = new BillPayorBillService(_context);
            BillCoOwnerService = new BillCoOwnerService(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
    