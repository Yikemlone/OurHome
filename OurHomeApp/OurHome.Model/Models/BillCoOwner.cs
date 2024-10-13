namespace OurHome.Models.Models
{
    /// <summary>
    /// Keeps track of the co-owners of a bill and the price that each co-owner payed.
    /// </summary>
    public class BillCoOwner
    {
        public int BillID { get; set; } // Fk
        public Bill? Bill { get; set; } // Navigation property

        public Guid UserID { get; set; } // Fk
        public User? User { get; set; } // Navigation property

        public decimal? Price { get; set; } // The price that the co-owner payed
    }
}
    