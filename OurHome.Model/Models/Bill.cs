using OurHome.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace OurHome.Models.Models
{
    /// <summary>
    /// This class represents a bill that is owed by one or more users.
    /// </summary>
    public class Bill
    {
        [Key]
        public int ID { get; set; } // Pk

        public string BillName { get; set; } // This is the name of the bill
        public DateTime DateTime { get; set; } // This may need to be nullable, as the bill may not have a specific date
        public decimal? Price { get; set; } // This the total price of the bill, it is nullable for cases where the bill price is variable
        public bool Reoccurring { get; set; } // If true, then the bill is reoccurring, otherwise it is a one time bill
        public bool SplitBill { get; set; } // If true, then the bill price is split evenly between all payors, otherwise the bill is the full price for each payor
        public string? Note { get; set; } // This is a note that the bill owner can add to the bill to give more information about the bill


        public Guid BillOwnerID { get; set; } // Fk
        public User? BillOwner { get; set; } // Navigation property

        public int HomeID { get; set; } // Fk
        public Home? Home { get; set; } // Navigation property

        public List<User>? CoOwners { get; set; } // This is a list of users that are co-owners of the bill
    }
}
