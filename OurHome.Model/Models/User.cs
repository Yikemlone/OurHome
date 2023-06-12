using Microsoft.AspNetCore.Identity;

namespace OurHome.Models.Models
{
    public class User : IdentityUser<Guid>
    {
        public List<Bill>? Bills { get; set; }
        public List<UserBill>? UserBills { get; set; }
    }
}
