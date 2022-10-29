using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurHome.Shared.DTO
{
    public class PersonDto
    {
        public int PersonID { get; set; }
        public string PersonName { get; set; }

    }
}
