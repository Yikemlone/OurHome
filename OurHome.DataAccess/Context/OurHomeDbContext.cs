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
        //public DbSet<HomeUsers> HomeUsers { get; set; }
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


            // Inviatations config
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

            // Bills and co-owners config
            builder.Entity<User>()
                .HasMany(b => b.UserBills)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserID);

            builder.Entity<User>()
                .HasMany(b => b.BillCoOwners)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserID);


            // Cascade deletes

            //builder.Entity<User>()
            //    .HasMany(h => h.HomeUsers)
            //    .WithOne(u => u.User)
            //    .HasForeignKey(fk => fk.UserID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<Home>()
            //    .HasMany(h => h.HomeUsers)
            //    .WithOne(h => h.Home)
            //    .HasForeignKey(fk => fk.HomeID)
            //    .OnDelete(DeleteBehavior.ClientCascade);

            //// This doesn't make sense to me yet, need to research FluentAPI

            //builder.Entity<HomeUsers>()
            //    .HasOne(u => u.User)
            //    .WithMany(u => u.HomeUsers)
            //    .HasForeignKey(u => u.UserID)
            //    .OnDelete(DeleteBehavior.ClientCascade);

            //builder.Entity<HomeUsers>()
            //    .HasOne(u => u.Home)
            //    .WithMany(u => u.HomeUsers)
            //    .HasForeignKey(u => u.HomeID)
            //    .OnDelete(DeleteBehavior.ClientCascade);


            // Many-to-many config 
            builder.Entity<Home>()
                .HasMany(u => u.Users)
                .WithMany(u => u.Homes)
                .UsingEntity<HomeUsers>();

            builder.Entity<Bill>()
                .HasMany(u => u.BillCoOwners)
                .WithMany(u => u.BillCoOwners)
                .UsingEntity<HomeUsers>();
        }
    }
}
