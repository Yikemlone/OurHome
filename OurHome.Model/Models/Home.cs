
using OurHome.Models.Models;

namespace OurHome.Model.Models
{
    public class Home
    {
        public int ID { get; set; }

        public string Name { get; set; }

        // This may cause cascading problems, must fix in OnModelCreate
        public Guid UserID { get; set; }
        public User? User { get; set; }

        public List<HomeUsers>? HomeUsers { get; set; }
        public List<HomeBill>? HomeBills { get; set; }
        public List<Invitation>? Invitations { get; set; }
    }
}
