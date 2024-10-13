using OurHome.Models.Models;

namespace OurHome.Shared.DTO
{
    public class BillDTO
    {
        public int ID { get; set; } 
        public string BillName { get; set; } 
        public DateTime DateTime { get; set; } 
        public decimal? Price { get; set; } 
        public bool Reoccurring { get; set; } 
        public string? Note { get; set; } 
        public Guid BillOwnerID { get; set; } 
        public int HomeID { get; set; }
        public List<User>? CoOwners { get; set; } 
    }
}