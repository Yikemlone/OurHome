using OurHome.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OurHome.DataAccess.Context
{
    public class OurHomeDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        DbSet<Bill> Bills { get; set; }
        DbSet<UserBill> UserBills { get; set; }

        public OurHomeDbContext(DbContextOptions options) : base(options)
        {
        }

        // TODO
        // Figure out how to add new users to the database using OnModelCreate
    }
}
