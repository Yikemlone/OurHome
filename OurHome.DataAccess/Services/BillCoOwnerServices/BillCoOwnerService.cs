using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using OurHome.DataAccess.Context;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.BillCoOwnerServices
{
    public class BillCoOwnerService : RepositoryService<BillCoOwner>, IBillCoOwnerService
    {
        private readonly OurHomeDbContext _context;

        public BillCoOwnerService(OurHomeDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<BillCoOwner>> AddAsync(List<User> coOwners, Bill bill)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<BillCoOwner>> AddAsync(List<User> coOwners, Bill bill)
        //{
        //    List<BillCoOwner> billCoOwners = new List<BillCoOwner>();

        //    foreach (var coOwner in coOwners)
        //    {
        //        billCoOwners.Add(new()
        //        {
        //            Bill = bill,
        //            Price = bill.Price/coOwners.Count,
        //            User = coOwner
        //        });
        //    }

        //    await _context.BillCoOwners.AddRangeAsync(billCoOwners);

        //    return billCoOwners;
        //}
    }
}
