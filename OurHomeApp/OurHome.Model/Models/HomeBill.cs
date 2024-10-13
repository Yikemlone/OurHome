namespace OurHome.Models.Models
{
    /// <summary>
    /// Bills that are associated with a home that the users of the home can select from when creating a new bill.
    /// </summary>
    public class HomeBill
    {
        public int ID { get; set; } // PK

        public string BillName { get; set; } // This is the name of the bill
        public DateTime DueDate { get; set; } // This is the date that the bill is due
        public decimal? Price { get; set; } // This is the price of the bill
        public bool PriceVaries { get; set; } // This is true if the price of the bill varies, otherwise it is false

        public int HomeID { get; set; } // FK 
        public Home? Home { get; set; } // Navigation property
    }
}
