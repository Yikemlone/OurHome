
using OurHome.Models.Models;

namespace OurHome.Model.Models
{
    public class BillCoOwner
    {
        public int BillID { get; set; }
        public Guid UserID { get; set; }

        public decimal? Price { get; set; }
    }
}
    