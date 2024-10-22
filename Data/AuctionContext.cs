using AuctionManagementAPI.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace AuctionManagementAPI.Data
{
    public class AuctionContext : DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options) { }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<AuctionSchedule> auctionSchedules { get; set; }
        public DbSet<Lable> Lables { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Otp> Otps { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Auction and Bid have a one-to-many relationship
            modelBuilder.Entity<Auction>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.Auction)
                .HasForeignKey(b => b.AuctionId);

            // Auction and AuctionSchedule have a one-to-one relationship
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.AuctionSchedule)
                .WithOne(s => s.Auction)
                .HasForeignKey<AuctionSchedule>(s => s.AuctionId);

            // Auction and Lable have a many-to-many relationship
            modelBuilder.Entity<Auction>()
                .HasMany(a => a.Lables)
                .WithMany(l => l.Auctions)
                .UsingEntity(j => j.ToTable("AuctionLables"));

            // Product and Auction have a one-to-many relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Auctions)
                .WithOne(a => a.Product)
                .HasForeignKey(a => a.ProductId);

            // Payment and Auction have a one-to-one relationship
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Auction)
                .WithOne(a => a.Payment)
                .HasForeignKey<Payment>(p => p.AuctionId)
                .OnDelete(DeleteBehavior.NoAction);

            // Category and Product  have a one-to-many relationship
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // Configure Category self-referencing relationship
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId);

            // Payment and Transaction have a one-to-many relationship
            modelBuilder.Entity<Payment>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Payment)
                .HasForeignKey(t => t.PaymentId);

            // Payment and Bid have a one-to-one relationship
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Bid)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BidId)
                .OnDelete(DeleteBehavior.Restrict);

            // User and Payment have a one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Payments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            // User and Bid have a one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bids)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            // User and Report have a one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reports)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            // UserRole and User have a one-to-many relationship
            modelBuilder.Entity<UserRole>()
                .HasMany(r => r.Users)
                .WithOne(u => u.UserRole)
                .HasForeignKey(u => u.UserRoleId);

            // UserRole and Permission have a many-to-many relationship
            modelBuilder.Entity<UserRole>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.UserRoles)
                .UsingEntity(j => j.ToTable("UserRolePermissions"));

            // User and UserProfile have a one-to-one relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // when a user is deleted, delete the profile too

            // User and Notification have a one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId);

            // User and ActivityLog have a one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.ActivityLogs)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            // User and Otp have a one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Otps)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);



            // Seed the buyer and admin roles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserRoleId = 1, Role = "admin" },
                new UserRole { UserRoleId = 2, Role = "buyer" }
            );

            // Seed an admin user with pre-hashed password and salt
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@lansuwa.com",
                    Password = "KtmYrvqbhm6PbytrQiY/e/StPH2XqMJNk4KVer4+AaY=", //lansuwa123
                    PasswordSalt = "T2NxIwaC6pQOcB5Z6C19rQ==", 
                    UserRoleId = 1,  // admin role
                    IsVerified = true  // Already verified
                }
            );

        }
    }

}
