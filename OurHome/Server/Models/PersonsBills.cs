using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurHome.Server.Models
{
    public class PersonsBills
    {
        [Key]
        [Column(TypeName = "int")]
        public int PersonID { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal Rent { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal Internet { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal Bins { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal? Electricity { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal? Oil { get; set; }
    }
}
