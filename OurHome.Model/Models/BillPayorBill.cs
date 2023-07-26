
namespace OurHome.Models.Models
{
    public class BillPayorBill
    {
        public int ID { get; set; }

        public string? PaymentType { get; set; } // Consider ENUM or another table
        public DateTime? DatePayed { get; set; } 
        public bool Payed { get; set; }
        public string? PersonalNote { get; set; }
        public decimal? UserPrice { get; set; }
        public bool PendingApproval { get; set; }


        public Guid PayeeID { get; set; }
        public User? Payee { get; set; }

        public Guid PayorID { get; set; }
        public User? Payor { get; set; }

        public int BillID { get; set; }
        public Bill? Bill { get; set; }
    }
}