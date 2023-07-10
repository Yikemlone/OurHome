using Microsoft.AspNetCore.Identity;
using OurHome.Model.Models;

namespace OurHome.Models.Models
{
    public class User : IdentityUser<Guid>
    {
        // User owned
        public List<Bill>? BillsOwned { get; set; }
        public List<BillPayor>? BillPayors { get; set; }
        public List<Home>? HomesOwned { get; set; }

        // Invitations 
        public List<Invitation>? ReceivedInvitations { get; set; }
        public List<Invitation>? SentInvitations { get; set; }

        // Homes Joined
        // This is a nav property needed for a many-to-many
        public List<HomeUsers>? HomesJoined { get; set; }
        public List<Home>? Homes { get; set; }

    }
}
