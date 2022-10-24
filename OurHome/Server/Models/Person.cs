using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurHome.Server.Models
{
    public class Person
    {
        [Key]
        [Column(TypeName = "int")]
        public int PersonID { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public int PersonName { get; set; }

    }
}