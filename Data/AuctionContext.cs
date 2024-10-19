using Microsoft.EntityFrameworkCore;

namespace YourNamespace.Models
{
    public class AuctionContext : DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionSchedule> AuctionSchedules { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<OTP> OTPs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User and UserRole relationship (many-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithMany(ur => ur.Users);

            // User and Notification relationship (one-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserID);

            // User and ActivityLog relationship (one-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.ActivityLogs)
                .WithOne(al => al.User)
                .HasForeignKey(al => al.UserID);

            // User and OTP relationship (one-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.OTPs)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserID);

            // UserRole and RolePermission relationship (one-to-many)
            modelBuilder.Entity<UserRole>()
                .HasMany(ur => ur.RolePermissions)
                .WithOne(rp => rp.UserRole)
                .HasForeignKey(rp => rp.RoleID);

            // Permission and RolePermission relationship (one-to-many)
            modelBuilder.Entity<Permission>()
                .HasMany(p => p.RolePermissions)
                .WithOne(rp => rp.Permission)
                .HasForeignKey(rp => rp.PermissionID);

            // UserProfile and User relationship (one-to-one)
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithOne(u => u.UserProfile)
                .HasForeignKey<UserProfile>(up => up.UserID);

            // Auction and Bid relationship (one-to-many)
            modelBuilder.Entity<Auction>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.Auction)
                .HasForeignKey(b => b.AuctionID);

            // Auction and User relationship (one-to-many)
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.User)
                .WithMany(u => u.Auctions)
                .HasForeignKey(a => a.User.UserID);

            // Auction and Category relationship (one-to-many)
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Auctions)
                .HasForeignKey(a => a.Category.CategoryID);

            // Auction and Label relationship (many-to-many)
            modelBuilder.Entity<Auction>()
                .HasMany(a => a.Labels)
                .WithMany(l => l.Auctions);

            // Auction and AuctionSchedule relationship (one-to-one)
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.AuctionSchedule)
                .WithOne(auctionSchedule => auctionSchedule.Auction)
                .HasForeignKey<AuctionSchedule>(auctionSchedule => auctionSchedule.AuctionID);

            // Bid and User relationship (one-to-many)
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Buyer)
                .WithMany(u => u.Bids)
                .HasForeignKey(b => b.BuyerID);

            // Product and User relationship (one-to-many)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.SellerID);

            // Product and Category relationship (one-to-many)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID);

            // Category self-referencing relationship (one-to-many)
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryID);

            // Payment and User relationship (one-to-many)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Buyer)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.BuyerID);

            // Payment and Auction relationship (one-to-many)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Auction)
                .WithMany(a => a.Payments)
                .HasForeignKey(p => p.AuctionID);

            // Payment and Transaction relationship (one-to-one)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Transaction)
                .WithOne(t => t.Payment)
                .HasForeignKey<Transaction>(t => t.PaymentID);

            // Transaction and Bid relationship (one-to-one)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Bid)
                .WithOne(b => b.Transaction)
                .HasForeignKey<Transaction>(t => t.BidID);

            // Report and User relationship (one-to-many)
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Admin)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.Admin.UserID);
        }
    }
}
