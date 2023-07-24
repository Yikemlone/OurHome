using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillCoOwnerService
{
    public interface IBillCoOwnerService
    {
        Task<List<BillCoOwner>> AddAllAsync(List<User> coOwners, Bill bill);
    }
}

