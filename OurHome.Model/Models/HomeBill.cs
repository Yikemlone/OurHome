using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurHome.Model.Models
{
    /// <summary>
    /// This is for admin use only. Creates default bills that can be used by the users to 
    /// choose from an options of bills as default. 
    /// </summary>
    public class HomeBill
    {
        public int ID { get; set; }

        public string BillName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal? Price { get; set; }
        public bool PriceVaries { get; set; } 
    }
}
