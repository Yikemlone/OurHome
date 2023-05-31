using System.ComponentModel.DataAnnotations;

namespace OurHome.Models.Models
{
    public class Bill
    {
        [Key]
        public int ID { get; set; }

        public string BillName { get; set; }
        public DateTime DateTime { get; set; }
        public decimal? Price { get; set; }
        public bool Reoccurring { get; set; }

    }
}
