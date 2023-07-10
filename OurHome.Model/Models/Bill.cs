using OurHome.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace OurHome.Models.Models
{
    public class Bill
    {
        [Key]
        public int ID { get; set; }

        public string BillName { get; set; }
        public DateTime DateTime { get; set; }
        public decimal? Price { get; set; }
        public bool Reoccurring { get; set; }
        public bool SplitBill { get; set; }
        public string Note { get; set; }


        public Guid BillOwnerID { get; set; }
        public User? BillOwner { get; set; }

        public int HomeID { get; set; }
        public Home? Home { get; set; }

        public List<User>? BillCoOwners { get; set; }
    }
}
