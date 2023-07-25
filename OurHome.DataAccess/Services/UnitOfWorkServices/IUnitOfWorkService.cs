using OurHome.DataAccess.Services.BillCoOwnerServices;
using OurHome.DataAccess.Services.BillPayorBillServices;
using OurHome.DataAccess.Services.HomeBillServices;
using OurHome.DataAccess.Services.UserServices;
using OurHome.Server.Services.BillsServices;

namespace OurHome.DataAccess.Services.UnitOfWorkServices
{
    public interface IUnitOfWorkService : IDisposable
    {
        IBillsService BillService { get; }
        IBillPayorBillService BillPayorBillService { get; }
        IBillCoOwnerService BillCoOwnerService { get; }

        Task Save();
    }
}
