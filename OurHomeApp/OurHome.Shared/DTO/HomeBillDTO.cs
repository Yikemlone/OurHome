namespace OurHome.Shared.DTO
{
    public class HomeBillDTO
    {
        public int ID { get; set; } // PK
        public string BillName { get; set; } // This is the name of the bill
        public DateTime DueDate { get; set; } // This is the date that the bill is due
        public decimal? Price { get; set; } // This is the price of the bill
        public bool PriceVaries { get; set; } // This is true if the price of the bill varies, otherwise it is false
        public int HomeID { get; set; } // FK 
    }
}