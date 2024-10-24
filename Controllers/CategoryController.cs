using Microsoft.AspNetCore.Mvc;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Dtos;
using AuctionManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AuctionContext _context;

        public CategoryController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.SubCategories)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Description = c.Description,
                    SubCategories = c.SubCategories.Select(sc => new CategoryDto
                    {
                        CategoryId = sc.CategoryId,
                        Name = sc.Name,
                        Description = sc.Description
                    }).ToList()
                }).ToListAsync();

            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                SubCategories = category.SubCategories.Select(sc => new CategoryDto
                {
                    CategoryId = sc.CategoryId,
                    Name = sc.Name,
                    Description = sc.Description
                }).ToList()
            };

            return Ok(categoryDto);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var category = new Category
            {
                Name = categoryCreateDto.Name,
                Description = categoryCreateDto.Description,
                ParentCategoryId = categoryCreateDto.ParentCategoryId
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var categoryDto = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };

            return CreatedAtAction(nameof(GetCategory), new { id = categoryDto.CategoryId }, categoryDto);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryUpdateDto.Name;
            category.Description = categoryUpdateDto.Description;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            // Check if the category has subcategories
            if (category.SubCategories.Any())
            {
                return BadRequest("Cannot delete a category with subcategories.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}