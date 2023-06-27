using OurHome.Models.Models;

namespace OurHome.Model.Models
{
    public class HomeUsers
    {
        public Guid UserID { get; set; }
        public User User { get; set; }

        public int HomeID { get; set; }
        public Home Home { get; set; }
    }
}
