using System.ComponentModel.DataAnnotations;

namespace OurHome.Models.Models
{
    /// <summary>
    /// The bills users create to assign other users a the price owed to the creator.
    /// </summary>
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


        public Guid UserID { get; set; }
        public User? User { get; set; }

    }
}
