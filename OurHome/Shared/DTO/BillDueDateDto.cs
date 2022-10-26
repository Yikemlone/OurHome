using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurHome.Shared.DTO
{
    public class BillDueDateDto
    {
        public int BillID { get; set; }
        public DateTime BillDueDate { get; set; }
    }
}
