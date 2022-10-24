using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurHome.Server.Models
{
    public class PayedBills
    {
        [Key]
        [Column(TypeName = "int")]
        public int PayedBillID { get; set; }


        [Column(TypeName = "int")]
        public int PersonID { get; set; }
        public Person Person { get; set; }


        [Column(TypeName = "date")]
        public DateTime BillDate { get; set; } = DateTime.Now;


        [Column(TypeName = "varchar(50)")]
        public string Bill { get; set; }


        [Column(TypeName = "varchar(4)")]
        public string PaymentType { get; set; } = "card";

    }
}