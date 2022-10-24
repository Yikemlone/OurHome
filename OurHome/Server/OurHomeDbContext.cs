using Microsoft.EntityFrameworkCore;
using OurHome.Server.Models;
using System.Collections.Generic;

namespace OurHome.Server
{
    public class OurHomeDbContext : DbContext
    {
        public OurHomeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bills> Bills { get; set; }
        public DbSet<PersonsBills> PersonsBills { get; set; }
        public DbSet<PayedBills> PayedBills { get; set; }
        public DbSet<BillsDueDate> BillsDueDate { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PastBills> PastBills { get; set; }


    }
}
