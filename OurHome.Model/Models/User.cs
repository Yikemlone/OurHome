using Microsoft.AspNetCore.Identity;
using OurHome.Model.Models;

namespace OurHome.Models.Models
{
    public class User : IdentityUser<Guid>
    {
        // User owned
        public List<Bill>? BillsOwned { get; set; }
        public List<PayorBill>? PayorBills { get; set; }
        public List<Home>? HomesOwned { get; set; }

        // Invitations 
        public List<Invitation>? ReceivedInvitations { get; set; }
        public List<Invitation>? SentInvitations { get; set; }

        // Homes Joined
        public List<HomeUsers>? HomesJoined { get; set; }
    }
}
