using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurHome.Shared.DTO
{
    public class PastBillDto
    {
        public int PastBillID { get; set; }
        public DateTime BillMonth { get; set; }
        public decimal Rent { get; set; }
        public decimal Internet { get; set; }
        public decimal Bins { get; set; }
        public decimal? Oil { get; set; }
        public decimal? Electric { get; set; }
    }
}
