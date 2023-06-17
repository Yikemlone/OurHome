
using OurHome.Models.Models;

namespace OurHome.Model.Models
{
    public class HomeUsers
    {
        public int ID { get; set; }

        // NEED TO THINK ABOUT HOW AMDINS WORK IN MULTIPLE HOMES

        public Guid UserID { get; set; }
        public User? User { get; set; }

        public int HomeID { get; set; }
        public Home? Home { get; set; }
    }
}
