using OurHome.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OurHome.Shared.DTO
{
    public class InvitationDTO
    {
        public int ID { get; set; } // Pk
        public string Status { get; set; } // This is the status of the invitation, can be pending, accepted, or declined
        public Guid FromUserID { get; set; } // Fk
        public Guid ToUserID { get; set; } // Fk
        public int HomeID { get; set; } // Fk
    }
}