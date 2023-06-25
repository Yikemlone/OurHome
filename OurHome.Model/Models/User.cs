using Microsoft.AspNetCore.Identity;
using OurHome.Model.Models;

namespace OurHome.Models.Models
{
    public class User : IdentityUser<Guid>
    {
        // Need to think about renaming some relationships because I want bill co-owners and home owners.


        // User owned
        public List<Bill>? Bills { get; set; } // This is a problemo <---
        public List<UserBill>? UserBills { get; set; }

        // Homes 
        public List<Home>? Homes { get; set; }

        // Invitations 
        public List<Invitation>? ReceivedInvitations { get; set; }
        public List<Invitation>? SentInvitations { get; set; }
    }
}
