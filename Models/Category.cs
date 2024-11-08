﻿using System.Text.Json.Serialization;

namespace AuctionManagementAPI.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<string>? ImageUrls { get; set; }
        public List<string>? LogoImageUrls { get; set; }

        // Navigation properties
        public ICollection<Product>? Products { get; set; }
        public ICollection<Category>? SubCategories { get; set; } // Collection of child categories

        [JsonIgnore]
        public Category? ParentCategory { get; set; } // Navigation property for parent category

        // Foreign key
        public int? ParentCategoryId { get; set; } // Nullable FK for parent category



    }
}
