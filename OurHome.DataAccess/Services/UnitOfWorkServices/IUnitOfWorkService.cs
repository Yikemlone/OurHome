using OurHome.DataAccess.Services.HomeBillServices;
using OurHome.DataAccess.Services.UserBillServices;
using OurHome.DataAccess.Services.UserServices;
using OurHome.Server.Services.Bills;

namespace OurHome.DataAccess.Services.UnitOfWorkServices
{
    public interface IUnitOfWorkService : IDisposable
    {
        IBillsService BillsService { get; }
        IBillPayorService UserBillService { get; }
        IHomeBillService HomeBillService { get; }
        IUserService UserService { get; }


        Task<bool> Save();
    }
}
