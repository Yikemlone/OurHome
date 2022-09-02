using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurHome.Server.Models
{
    public class PersonsBills
    {
        [Key]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public decimal Rent { get; set; }
        public decimal Internet { get; set; }
        public decimal Bins { get; set; }
        public decimal Electricity { get; set; }
        public decimal Milk { get; set; }
        public decimal Oil { get; set; }
    }
}
