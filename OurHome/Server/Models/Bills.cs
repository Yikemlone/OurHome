using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurHome.Server.Models
{
    public class Bills
    {
        [Key]
        [Column(TypeName = "int")]
        public int BillID { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public string Bill { get; set; }


        [Column(TypeName = "decimal")]
        public decimal? Price { get; set; }

    }
}
