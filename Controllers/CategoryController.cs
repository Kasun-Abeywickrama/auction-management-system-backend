using AuctionManagementAPI.Models;
using AuctionManagementAPI.Models.DTOs.CategoryDTOs;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this._categoryService = categoryService;
        }


        // Get All Categories
        // Retrieve a flat list of all categories without any hierarchy.
        // Useful for displaying all categories at once, possibly in a dropdown list.
        // GET: api/Category/GetAllCategories
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            // get all categories
            var result = await _categoryService.GetAllCategoriesAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get categories");
            }

        }


        // Get Category with Subcategories (Recursive)
        // Retrieve a category along with its hierarchy of subcategories.
        // This recursive method allows you to see a nested structure of categories and subcategories.
        // GET: api/Category/GetCategoryWithSubcategories
        [HttpGet("GetCategoryWithSubcategories")]
        public async Task<IActionResult> GetCategoryWithSubcategories(int categoryId)
        {
            // get category with subcategories
            var result = await _categoryService.GetCategoryWithSubcategoriesAsync(categoryId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get category with subcategories");
            }

        }


        // Get All Categories with Nested Subcategories
        // Retrieves all categories and subcategories in a nested(hierarchical) format.
        // Useful for displaying the complete category hierarchy.
        // GET: api/Category/GetAllCategoriesWithSubcategories
        [HttpGet("GetAllCategoriesWithSubcategories")]
        public async Task<IActionResult> GetAllCategoriesWithSubcategories()
        {
            // get all categories with nested subcategories
            var result = await _categoryService.GetAllCategoriesWithHierarchyAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get categories with subcategories");
            }

        }


        // Get Parent Category of a Specific Category
        // Retrieve the parent category of a specific category by its CategoryId.
        // Useful for breadcrumb navigation.
        // GET: api/Category/GetParentCategory
        [HttpGet("GetParentCategory")]
        public async Task<IActionResult> GetParentCategory(int categoryId)
        {
            // get parent category
            var result = await _categoryService.GetParentCategoryAsync(categoryId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get parent category");
            }

        }


        // Get All Top-Level Categories
        // Retrieves only the top-level categories(those with no ParentCategoryId).
        // Useful for displaying only root categories without any hierarchy.
        // GET: api/Category/GetTopLevelCategories
        [HttpGet("GetTopLevelCategories")]
        public async Task<IActionResult> GetTopLevelCategories()
        {
            // get all top-level categories
            var result = await _categoryService.GetTopLevelCategoriesAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get top-level categories");
            }

        }


        // Get Subcategories for a Specific Category
        // Retrieve only the direct subcategories of a given category.
        // Useful for a drill-down interface where the user selects a category, and subcategories load dynamically.
        // GET: api/Category/GetSubcategories
        [HttpGet("GetSubcategories")]
        public async Task<IActionResult> GetSubcategories(int categoryId)
        {
            // get subcategories for a specific category
            var result = await _categoryService.GetSubcategoriesAsync(categoryId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get subcategories");
            }

        }


        // Get Category Path (Breadcrumb)
        // Retrieve the entire path from the current category up to the root, which can be useful for breadcrumbs.
        // GET: api/Category/GetCategoryPath
        [HttpGet("GetCategoryPath")]
        public async Task<IActionResult> GetCategoryPath(int categoryId)
        {
            // get category path
            var result = await _categoryService.GetCategoryPathAsync(categoryId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get category path");
            }

        }


        // POST: api/Category/CreateCategory
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            // check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // create the category
            var result = await _categoryService.CreateCategoryAsync(categoryDTO);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to create category");
            }

        }


        // PUT: api/Category/UpdateCategory
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO categoryDTO, int categoryId)
        {
            // check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // update the category
            var result = await _categoryService.UpdateCategoryAsync(categoryDTO, categoryId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to update category");
            }

        }


        // DELETE: api/Category/DeleteCategory
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            // delete the category
            var result = await _categoryService.DeleteCategoryAsync(categoryId);

            if (result)
            {
                return Ok("Category deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete category");
            }
        }

    }
}
