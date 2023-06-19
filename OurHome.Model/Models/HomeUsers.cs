
using OurHome.Models.Models;

namespace OurHome.Model.Models
{
    public class HomeUsers
    {
        public int ID { get; set; }

        // NEED TO THINK ABOUT HOW AMDINS WORK IN MULTIPLE HOMES

        // Maybe create a claim thay checks if current home ID and thoese users if they have admin?

        public Guid UserID { get; set; }
        public User? User { get; set; }

        public int HomeID { get; set; }
        public Home? Home { get; set; }
    }
}
