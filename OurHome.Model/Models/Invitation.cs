namespace OurHome.Models.Models
{
    /// <summary>
    /// Keeps track of the invitations that are sent between users.
    /// </summary>
    public class Invitation
    {
        public int ID { get; set; } // Pk

        public string Status { get; set; } // This is the status of the invitation, can be pending, accepted, or declined

        public Guid FromUserID { get; set; } // Fk
        public User? FromUser { get; set; } // Navigation property

        public Guid ToUserID { get; set; } // Fk
        public User? ToUser { get; set; } // Navigation property

        public int HomeID { get; set; } // Fk
        public Home? Home { get; set; } // Navigation property
    }
}
  