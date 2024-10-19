using AuctionManagementAPI.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace AuctionManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }

}
