using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Model.Models;

namespace OurHome.DataAccess.Services.HomeBillServices
{
    public class HomeBillService : IHomeBillService
    {
        private readonly OurHomeDbContext _context;


        public HomeBillService(OurHomeDbContext context)
        { 
            _context = context;
        }

        public Task<HomeBill> AddAsync(HomeBill obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(HomeBill obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HomeBill>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HomeBill> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HomeBill> UpdateAsync(HomeBill obj)
        {
            throw new NotImplementedException();
        }
    }
}
