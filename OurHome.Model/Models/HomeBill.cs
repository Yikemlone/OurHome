
namespace OurHome.Model.Models
{
    public class HomeBill
    {
        public int ID { get; set; }

        public string BillName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal? Price { get; set; }
        public bool PriceVaries { get; set; }

        public int HomeID { get; set; }
        public Home? Home { get; set; }
    }
}
