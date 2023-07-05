﻿using OurHome.Models.Models;
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
        public DbSet<HomeUsers> HomeUsers { get; set; }
        public DbSet<Invitation> Invations { get; set; }
        public DbSet<PayorBill> PayorBills { get; set; }

        public OurHomeDbContext(DbContextOptions options) : base(options)
        {
        }

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

            builder.Entity<User>()
                .HasMany(b => b.PayorBills)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Bill>()
                .HasOne(b => b.User)
                .WithMany(b => b.BillsOwned)
                .HasForeignKey(b => b.BillOwnerID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Home>()
                .HasMany(u => u.Users)
                .WithMany(u => u.Homes)
                .UsingEntity<HomeUsers>();

            builder.Entity<Home>()
                .HasOne(h => h.HomeOwnerUser)
                .WithMany(u => u.HomesOwned)
                .HasForeignKey(fk => fk.HomeOwnerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
