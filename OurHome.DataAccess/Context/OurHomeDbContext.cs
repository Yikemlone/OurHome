using OurHome.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurHome.Model.Models;
using Microsoft.Data.SqlClient;

namespace OurHome.DataAccess.Context
{
    public class OurHomeDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillCoOwner> BillCoOwners { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<HomeBill> HomeBills { get; set; }
        public DbSet<HomeUser> HomeUsers { get; set; }
        public DbSet<Invitation> Invations { get; set; }
        public DbSet<BillPayorBill> BillPayors { get; set; }

        public OurHomeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // A user can have many invations sent to one user at a time
            builder.Entity<User>()
                .HasMany(a => a.SentInvitations)
                .WithOne(a => a.ToUser)
                .HasForeignKey(a => a.ToUserID)
                .OnDelete(DeleteBehavior.Restrict);

            // A user can receive many invations from one user at a time
            builder.Entity<User>()
                .HasMany(a => a.ReceivedInvitations)
                .WithOne(a => a.FromUser)
                .HasForeignKey(a => a.FromUserID)
                .OnDelete(DeleteBehavior.Restrict);

            // No idea
            builder.Entity<User>()
                .HasMany(b => b.BillPayors) 
                .WithOne(b => b.Payor)
                .HasForeignKey(b => b.PayorID)
                .OnDelete(DeleteBehavior.Restrict);
            
            // No idea
            builder.Entity<User>()
                .HasMany(b => b.BillPayees)
                .WithOne(b => b.Payee)
                .HasForeignKey(b => b.PayeeID)
                .OnDelete(DeleteBehavior.Restrict);

            // A user can have many co-owned bills at a time
            builder.Entity<User>()
                .HasMany(a => a.BillsCoOwned)
                .WithMany(a => a.CoOwners);

            // A user can own many bills at a time
            builder.Entity<Bill>()
                .HasOne(b => b.BillOwner)
                .WithMany(b => b.BillsOwned)
                .HasForeignKey(b => b.BillOwnerID)
                .OnDelete(DeleteBehavior.Restrict);

            // Many users can be apart of many homes at a time
            builder.Entity<Home>()
                .HasMany(u => u.Users)
                .WithMany(u => u.Homes)
                .UsingEntity<HomeUser>();

            // Bill can have many co-owners at a time
            builder.Entity<Bill>()
                .HasMany(u => u.CoOwners)
                .WithMany(u => u.BillsCoOwned)
                .UsingEntity<BillCoOwner>();

            // A user can own many homes at a time
            builder.Entity<Home>()
                .HasOne(h => h.HomeOwner)
                .WithMany(u => u.HomesOwned)
                .HasForeignKey(fk => fk.HomeOwnerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
