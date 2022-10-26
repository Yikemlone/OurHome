using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurHome.Shared.DTO
{
    public class PayedBillDto
    {
        public int PayedBillID { get; set; }
        public int PersonID { get; set; }
        public DateTime BillDate { get; set; } = DateTime.Now;
        public string Bill { get; set; }
        public string PaymentType { get; set; } = "card";

    }
}
