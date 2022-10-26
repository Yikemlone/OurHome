using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OurHome.Server.Models
{
    public class BillsDueDate
    {
        [Key]
        [Column(TypeName = "int")]
        public int BillID { get; set; }


        [Column(TypeName = "date")]
        public DateTime BillDueDate { get; set; }

    }
}
