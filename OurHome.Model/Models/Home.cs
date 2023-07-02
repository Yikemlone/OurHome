﻿
using OurHome.Models.Models;

namespace OurHome.Model.Models
{
    public class Home
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Guid HomeOwnerID { get; set; }
        public User User { get; set; }


        // This is a nav property needed for a many-to-many
        public List<HomeUsers>? HomeUsers { get; set; }
        public List<User> Users { get; set; }

        public List<HomeBill>? HomeBills { get; set; }
        public List<Invitation>? Invitations { get; set; }
    }
}
