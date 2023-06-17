using Microsoft.AspNetCore.Identity;
using OurHome.Model.Models;

namespace OurHome.Models.Models
{
    public class User : IdentityUser<Guid>
    {
        public List<Bill>? Bills { get; set; }
        public List<UserBill>? UserBills { get; set; }
        public List<HomeUsers>? HomeUsers { get; set; }

    }
}
