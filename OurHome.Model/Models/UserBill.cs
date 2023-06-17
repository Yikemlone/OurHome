
namespace OurHome.Models.Models
{
    public class UserBill
    {
        public int ID { get; set; }

        public string PaymentType { get; set; }
        public DateTime DatePayed { get; set; }
        public bool Payed { get; set; }
        public string PersonalNote { get; set; }
        public decimal? UserPrice { get; set; }
        public bool PendingApproval { get; set; } 

        public Guid UserID { get; set; }
        public User? User { get; set; }

        public int BillID { get; set; }
        public Bill? Bill { get; set; }
    }
}