using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.Shared.DTO
{
    public class CreateBillDTO
    {
        public Bill Bill { get; set; }
        public List<User> BillPayors { get; set; }
        public List<BillCoOwner>? BillCoOwners { get; set; }
    }
}
