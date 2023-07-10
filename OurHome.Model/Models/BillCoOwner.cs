
using OurHome.Models.Models;

namespace OurHome.Model.Models
{
    public class BillCoOwner
    {
        public int ID { get; set; }

        public int BillID { get; set; }
        public Bill? Bill { get; set; }

        public Guid UserID { get; set; }
        public User? User { get; set; }

        public decimal? Price { get; set; }
    }
}
    