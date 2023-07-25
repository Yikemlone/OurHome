using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillCoOwnerServices
{
    public interface IBillCoOwnerService : IRepositoryService<BillCoOwner>
    {
        Task<List<BillCoOwner>> AddAsync(List<User> coOwners, Bill bill);
    }
}

