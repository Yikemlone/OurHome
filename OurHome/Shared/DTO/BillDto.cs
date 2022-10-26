using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurHome.Shared.DTO
{
    public class BillDto
    {
        public int BillID { get; set; }
        public string Bill { get; set; }
        public decimal? Price { get; set; }
    }
}
