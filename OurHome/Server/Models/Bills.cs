using System.ComponentModel.DataAnnotations;

namespace OurHome.Server.Models
{
    public class Bills
    {
        [Key]
        public int ID { get; set; }
        public string Bill { get; set; }
        public decimal Price { get; set; }

    }
}
