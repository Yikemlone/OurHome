using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurHome.Shared.DTO
{
    public class BillsDto
    {
        public int ID { get; set; }
        public string Bill { get; set; }
        public decimal Price { get; set; }
    }
}
