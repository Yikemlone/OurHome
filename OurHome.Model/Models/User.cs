using Microsoft.AspNetCore.Identity;
using OurHome.Model.Models;

namespace OurHome.Models.Models
{
    public class User : IdentityUser<Guid>
    {
        public List<Bill>? BillsOwned { get; set; }
        public List<BillPayorBill>? BillPayors { get; set; }
        public List<BillPayorBill>? BillPayees { get; set; }
        public List<Home>? HomesOwned { get; set; }
        public List<Bill>? BillsCoOwned { get; set; }
        public List<Invitation>? ReceivedInvitations { get; set; }
        public List<Invitation>? SentInvitations { get; set; }
        public List<HomeUser>? HomesJoined { get; set; }
        public List<Home>? Homes { get; set; }
    }
}
