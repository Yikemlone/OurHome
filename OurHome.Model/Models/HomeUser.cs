using OurHome.Models.Models;

namespace OurHome.Model.Models
{
    /// <summary>
    /// This keeps track of the users that are members of a home. Used for many to many relationship between users and homes.
    /// </summary>
    public class HomeUser
    {
        public Guid UserID { get; set; } // Fk
        public User User { get; set; } // Navigation property

        public int HomeID { get; set; } // Fk
        public Home Home { get; set; } // Navigation property
    }
}
