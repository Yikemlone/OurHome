using OurHome.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OurHome.DataAccess.Context
{
    public class OurHomeDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Bill> PersonsBills { get; set; }
        public DbSet<UserBill> Person { get; set; }

        public OurHomeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
