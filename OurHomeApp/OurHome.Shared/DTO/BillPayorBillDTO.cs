namespace OurHome.Shared.DTO
{
    public class BillPayorBillDTO
    {
        public int ID { get; set; } // Pk
        public string? PaymentType { get; set; } // Consider ENUM or another table
        public DateTime? DatePayed { get; set; } // This is the date that the payor payed the bill
        public bool Payed { get; set; } // This is true if the payor has payed the bill
        public string? PersonalNote { get; set; } // This is a note that the payor can add to the bill to give more information about the bill to the payee
        public decimal? UserPrice { get; set; }  // This is the price that the payor owes for the bill
        public bool PendingApproval { get; set; } // This is true if the payor has payed the bill and is waiting for the payee to approve the payment
        public Guid PayeeID { get; set; } // Fk
        public Guid PayorID { get; set; } // Fk
        public int BillID { get; set; } // Fk
    }
}