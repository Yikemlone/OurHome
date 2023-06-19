
using OurHome.Models.Models;

namespace OurHome.Model.Models
{
    public class Invitation
    {
        public int ID { get; set; }

        public string Status { get; set; }

        public Guid FromUserID { get; set; }
        public User? FromUser { get; set; }

        public Guid ToUserID { get; set; }
        public User? ToUser { get; set; }

        public int HomeID { get; set; }
        public Home? Home { get; set; }
    }
}
