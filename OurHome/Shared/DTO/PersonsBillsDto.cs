using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurHome.Shared.DTO
{
    public class PersonsBillsDto
    {
        public int PersonID { get; set; }
        public decimal Rent { get; set; }
        public decimal Internet { get; set; }
        public decimal Bins { get; set; }
        public decimal Electricity { get; set; }
        public decimal Milk { get; set; }
        public decimal Oil { get; set; }
    }
}
