using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.BillCoOwnerServices;
using OurHome.DataAccess.Services.BillPayorBillServices;
using OurHome.DataAccess.Services.BillsServices;
using OurHome.Server.Services.BillsServices;
using System.Runtime.CompilerServices;

namespace OurHome.DataAccess.Services.UnitOfWorkServices
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        public IBillsService BillService { get; private set; }
        public IBillPayorBillService BillPayorBillService { get; private set; }
        public IBillCoOwnerService BillCoOwnerService { get; private set; }


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

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
    