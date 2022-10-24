using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OurHome.Server.Models
{
    public class PastBills
    {
        [Key]
        [Column(TypeName = "int")]
        public int PastBillID { get; set; }


        [Column(TypeName = "date")]
        public DateTime BillMonth { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal Rent { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal Internet { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal Bins { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal? Oil { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal? Electric { get; set; }

    }
}
