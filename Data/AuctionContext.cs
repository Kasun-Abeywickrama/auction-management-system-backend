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
        public DbSet<WatchAuction> WatchAuctions { get; set; }


        public DbSet<ShippingDetails> ShippingDetails { get; set; }


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
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

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

            //User and ShippingDetails have a one-to-many relationship
            modelBuilder.Entity<ShippingDetails>()
                .HasOne(s => s.User)
                .WithMany(u => u.ShippingDetails)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bid and ShippingDetails have a one-to-one relationship
             modelBuilder.Entity<ShippingDetails>()
                .HasOne(b => b.Bid)
                .WithOne(s => s.shippingDetails)
                .HasForeignKey<ShippingDetails>(b => b.BidId)
                .OnDelete(DeleteBehavior.Restrict);



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


            // Seed permissions (Create all permissions for all Models such as "to_create_auction", "to_üpdate_auction", "to_delete_auction", "to_get_all_auctions", "to_get_auction_by_id" etc.)
            modelBuilder.Entity<Permission>().HasData(
                new Permission { PermissionId = 1, Name = "to_create_auction" },
                new Permission { PermissionId = 2, Name = "to_update_auction" },
                new Permission { PermissionId = 3, Name = "to_delete_auction" },
                new Permission { PermissionId = 4, Name = "to_get_all_auctions" },
                new Permission { PermissionId = 5, Name = "to_get_auction_by_id" },
                new Permission { PermissionId = 6, Name = "to_create_bid" },
                new Permission { PermissionId = 7, Name = "to_update_bid" },
                new Permission { PermissionId = 8, Name = "to_delete_bid" },
                new Permission { PermissionId = 9, Name = "to_get_all_bids" },
                new Permission { PermissionId = 10, Name = "to_get_bid_by_id" },
                new Permission { PermissionId = 11, Name = "to_create_auction_schedule" },
                new Permission { PermissionId = 12, Name = "to_update_auction_schedule" },
                new Permission { PermissionId = 13, Name = "to_delete_auction_schedule" },
                new Permission { PermissionId = 14, Name = "to_get_all_auction_schedules" },
                new Permission { PermissionId = 15, Name = "to_get_auction_schedule_by_id" },
                new Permission { PermissionId = 16, Name = "to_create_lable" },
                new Permission { PermissionId = 17, Name = "to_update_lable" },
                new Permission { PermissionId = 18, Name = "to_delete_lable" },
                new Permission { PermissionId = 19, Name = "to_get_all_lables" },
                new Permission { PermissionId = 20, Name = "to_get_lable_by_id" },
                new Permission { PermissionId = 21, Name = "to_create_product" },
                new Permission { PermissionId = 22, Name = "to_update_product" },
                new Permission { PermissionId = 23, Name = "to_delete_product" },
                new Permission { PermissionId = 24, Name = "to_get_all_products" },
                new Permission { PermissionId = 25, Name = "to_get_product_by_id" },
                new Permission { PermissionId = 26, Name = "to_create_payment" },
                new Permission { PermissionId = 27, Name = "to_update_payment" },
                new Permission { PermissionId = 28, Name = "to_delete_payment" },
                new Permission { PermissionId = 29, Name = "to_get_all_payments" },
                new Permission { PermissionId = 30, Name = "to_get_payment_by_id" },
                new Permission { PermissionId = 31, Name = "to_create_category" },
                new Permission { PermissionId = 32, Name = "to_update_category" },
                new Permission { PermissionId = 33, Name = "to_delete_category" },
                new Permission { PermissionId = 34, Name = "to_get_all_categories" },
                new Permission { PermissionId = 35, Name = "to_get_category_by_id" },
                new Permission { PermissionId = 36, Name = "to_create_transaction" },
                new Permission { PermissionId = 37, Name = "to_update_transaction" },
                new Permission { PermissionId = 38, Name = "to_delete_transaction" },
                new Permission { PermissionId = 39, Name = "to_get_all_transactions" },
                new Permission { PermissionId = 40, Name = "to_get_transaction_by_id" },
                new Permission { PermissionId = 41, Name = "to_create_user" },
                new Permission { PermissionId = 42, Name = "to_update_user" },
                new Permission { PermissionId = 43, Name = "to_delete_user" },
                new Permission { PermissionId = 44, Name = "to_get_all_users" },
                new Permission { PermissionId = 45, Name = "to_get_user_by_id" },
                new Permission { PermissionId = 46, Name = "to_create_report" },
                new Permission { PermissionId = 47, Name = "to_update_report" },
                new Permission { PermissionId = 48, Name = "to_delete_report" },
                new Permission { PermissionId = 49, Name = "to_get_all_reports" },
                new Permission { PermissionId = 50, Name = "to_get_report_by_id" },
                new Permission { PermissionId = 51, Name = "to_create_user_role" },
                new Permission { PermissionId = 52, Name = "to_update_user_role" },
                new Permission { PermissionId = 53, Name = "to_delete_user_role" },
                new Permission { PermissionId = 54, Name = "to_get_all_user_roles" },
                new Permission { PermissionId = 55, Name = "to_get_user_role_by_id" },
                new Permission { PermissionId = 56, Name = "to_create_permission" },
                new Permission { PermissionId = 57, Name = "to_update_permission" },
                new Permission { PermissionId = 58, Name = "to_delete_permission" },
                new Permission { PermissionId = 59, Name = "to_get_all_permissions" },
                new Permission { PermissionId = 60, Name = "to_get_permission_by_id" },
                new Permission { PermissionId = 61, Name = "to_create_user_profile" },
                new Permission { PermissionId = 62, Name = "to_update_user_profile" },
                new Permission { PermissionId = 63, Name = "to_delete_user_profile" },
                new Permission { PermissionId = 64, Name = "to_get_all_user_profiles" },
                new Permission { PermissionId = 65, Name = "to_get_user_profile_by_id" },
                new Permission { PermissionId = 66, Name = "to_create_notification" },
                new Permission { PermissionId = 67, Name = "to_update_notification" },
                new Permission { PermissionId = 68, Name = "to_delete_notification" },
                new Permission { PermissionId = 69, Name = "to_get_all_notifications" },
                new Permission { PermissionId = 70, Name = "to_get_notification_by_id" },
                new Permission { PermissionId = 71, Name = "to_create_activity_log" },
                new Permission { PermissionId = 72, Name = "to_update_activity_log" },
                new Permission { PermissionId = 73, Name = "to_delete_activity_log" },
                new Permission { PermissionId = 74, Name = "to_get_all_activity_logs" },
                new Permission { PermissionId = 75, Name = "to_get_activity_log_by_id" },
                new Permission { PermissionId = 76, Name = "to_create_otp" },
                new Permission { PermissionId = 77, Name = "to_update_otp" },
                new Permission { PermissionId = 78, Name = "to_delete_otp" },
                new Permission { PermissionId = 79, Name = "to_get_all_otps" },
                new Permission { PermissionId = 80, Name = "to_get_otp_by_id" }
                );

        }
    }

}
