using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;

namespace OurHome.DataAccess.Services.HomeUserServices
{
    public interface IHomeUserService : IRepositoryService<HomeUser>
    {
        Task AddAsync(Invitation invitation);
    }
}
