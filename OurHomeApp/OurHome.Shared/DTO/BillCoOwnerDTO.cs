namespace OurHome.Shared.DTO
{
    public class BillCoOwnerDTO
    {
        public int BillID { get; set; } // Fk
        public Guid UserID { get; set; } // Fk
        public decimal? Price { get; set; } // The price that the co-owner payed
    }
}
