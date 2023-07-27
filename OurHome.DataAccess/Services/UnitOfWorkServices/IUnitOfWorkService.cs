using OurHome.DataAccess.Services.BillCoOwnerServices;
using OurHome.DataAccess.Services.BillPayorBillServices;
using OurHome.DataAccess.Services.HomeBillServices;
using OurHome.DataAccess.Services.HomeServices;
using OurHome.DataAccess.Services.HomeUserServices;
using OurHome.DataAccess.Services.InvitationServices;
using OurHome.Server.Services.BillServices;

namespace OurHome.DataAccess.Services.UnitOfWorkServices
{
    public interface IUnitOfWorkService : IDisposable
    {
        IBillService BillService { get; }
        IBillPayorBillService BillPayorBillService { get; }
        IBillCoOwnerService BillCoOwnerService { get; }
        IHomeService HomeService { get; }
        IHomeBillService HomeBillService { get; }
        IHomeUserService HomeUserService { get; }
        IInvitationService InvitationService { get; }

        // I may not even need a user service, maybe for updating their details

        Task SaveAsync();
    }
}
