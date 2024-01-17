using Microsoft.AspNetCore.Identity;
using OurHome.Model.Models;

namespace OurHome.Models.Models
{
    /// <summary>
    /// This class represents a user of the application. 
    /// </summary>
    public class User : IdentityUser<Guid>
    {
        public List<Bill>? BillsOwned { get; set; } // List of bills that the user owns

        public List<BillPayorBill>? BillPayors { get; set; } 
        public List<BillPayorBill>? BillPayees { get; set; } 

        public List<Home>? HomesOwned { get; set; } // List of homes that the user owns
        public List<Bill>? BillsCoOwned { get; set; } // List of bills that the user is a co-owner of

        public List<Invitation>? ReceivedInvitations { get; set; } // List of invitations that the user has received
        public List<Invitation>? SentInvitations { get; set; } // List of invitations that the user has sent

        public List<HomeUser>? HomesJoined { get; set; } // List of homes that the user is a member of
        public List<Home>? Homes { get; set; } // Navigation property, needed for many to many relationship
    }
}
