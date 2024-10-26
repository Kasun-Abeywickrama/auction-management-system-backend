using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Models.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementAPI.Services
{
    public class CategoryService
    {
        private readonly AuctionContext _context;

        public CategoryService(AuctionContext context)
        {
            this._context = context;
        }

     
        // Get all categories
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }


        // Get category with subcategories (recursive)
        public async Task<Category> GetCategoryWithSubcategoriesAsync(int categoryId)
        {
            var category = await _context.Categories
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            if (category?.SubCategories != null)
            {
                foreach (var subCategory in category.SubCategories)
                {
                    await LoadSubcategoriesRecursive(subCategory);
                }
            }
            return category;
        }

        private async Task LoadSubcategoriesRecursive(Category category)
        {
            await _context.Entry(category).Collection(c => c.SubCategories).LoadAsync();
            if (category.SubCategories != null)
            {
                foreach (var subCategory in category.SubCategories)
                {
                    await LoadSubcategoriesRecursive(subCategory);
                }
            }
        }


        // Get all categories with nested subcategories
        public async Task<IEnumerable<Category>> GetAllCategoriesWithHierarchyAsync()
        {
            var rootCategories = await _context.Categories
                .Where(c => c.ParentCategoryId == null)
                .Include(c => c.SubCategories)
                .ToListAsync();

            foreach (var category in rootCategories)
            {
                await LoadSubcategoriesRecursive(category);
            }
            return rootCategories;
        }


        // Get Parent Category of a Specific Category
        public async Task<Category?> GetParentCategoryAsync(int categoryId)
        {
            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            return category?.ParentCategory;
        }


        // Get all top level categories
        public async Task<IEnumerable<Category>> GetTopLevelCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToListAsync();
        }


        // Get Subcategories for a Specific Category
        public async Task<IEnumerable<Category>> GetSubcategoriesAsync(int parentCategoryId)
        {
            return await _context.Categories
                .Where(c => c.ParentCategoryId == parentCategoryId)
                .ToListAsync();
        }


        // Get Category Path (Breadcrumb)
        public async Task<List<Category>> GetCategoryPathAsync(int categoryId)
        {
            var path = new List<Category>();
            var category = await _context.Categories.FindAsync(categoryId);
            while (category != null)
            {
                path.Insert(0, category);
                category = category.ParentCategoryId != null
                    ? await _context.Categories.FindAsync(category.ParentCategoryId)
                    : null;
            }
            return path;
        }


        // Create a new category
        public async Task<Category> CreateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
                ParentCategoryId = categoryDTO.ParentCategoryId

            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;

        }


        // Update an existing category
        public async Task<Category> UpdateCategoryAsync(CategoryDTO categoryDTO, int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return null;
            }

            category.Name = categoryDTO.Name;
            category.Description = categoryDTO.Description;
            category.ParentCategoryId = categoryDTO.ParentCategoryId;

            await _context.SaveChangesAsync();

            return category;
        }


        // Delete a category
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
