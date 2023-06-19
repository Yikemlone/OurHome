using OurHome.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurHome.Model.Models;

namespace OurHome.DataAccess.Context
{
    public class OurHomeDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillCoOwner> BillCoOwners { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<HomeBill> HomeBills { get; set; }
        public DbSet<HomeUsers> HomeUsers { get; set; }
        public DbSet<Invitation> Invations { get; set; }
        public DbSet<UserBill> UserBills { get; set; }

        public OurHomeDbContext(DbContextOptions options) : base(options)
        {
        }

        // TODO
        // Figure out how to add new users to the database using OnModelCreate
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(a => a.SentInvitations)
                .WithOne(a => a.ToUser)
                .HasForeignKey(a => a.ToUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(a => a.ReceivedInvitations)
                .WithOne(a => a.FromUser)
                .HasForeignKey(a => a.FromUserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
