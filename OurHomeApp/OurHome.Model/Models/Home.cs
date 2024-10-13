namespace OurHome.Models.Models
{
    /// <summary>
    /// This class represents a home, keeps track of the home owner, the users in the home, the bills in the home, and the invitations to the home.
    /// </summary>
    public class Home
    {
        public int ID { get; set; } // Pk

        public string Name { get; set; } // This is the name of the home

        // Could add other properties such as image, description, etc.

        public Guid HomeOwnerID { get; set; } // Fk
        public User?  HomeOwner { get; set; } // Navigation property

        public List<HomeUser>? HomeUsers { get; set; } // List of users in the home
        public List<User>? Users { get; set; } // Navigation property, needed for many to many relationship

        public List<HomeBill>? HomeBills { get; set; } // Bills that are in the home
        public List<Invitation>? Invitations { get; set; } // Invitations to the home
    }
}
