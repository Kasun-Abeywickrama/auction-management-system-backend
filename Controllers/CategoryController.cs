using AuctionManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AuctionManagementAPI.Data;
using System.Linq;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AuctionContext _dbContext;

        public CategoriesController(AuctionContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/categories (Show all categories)
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _dbContext.Categories.Select(c => new
            {
                c.CategoryId,
                c.Name,
                c.Description
            }).ToList();

            if (categories == null || categories.Count == 0)
            {
                return NoContent();
            }
            return Ok(categories);
        }

        // GET: api/categories/{id} (Show a specific category by ID)
        [HttpGet("{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _dbContext.Categories
                .Where(c => c.CategoryId == id)
                .Select(c => new
                {
                    c.CategoryId,
                    c.Name,
                    c.Description
                })
                .FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/categories (Add a new category)
        [HttpPost]
        public IActionResult AddCategory(AddCategoryDto addCategoryDto)
        {
            if (addCategoryDto == null)
            {
                return BadRequest("Category data is missing");
            }

            var categoryEntity = new Category()
            {
                Name = addCategoryDto.Name,
                Description = addCategoryDto.Description
            };

            _dbContext.Categories.Add(categoryEntity);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryEntity.CategoryId }, new
            {
                categoryEntity.CategoryId,
                categoryEntity.Name,
                categoryEntity.Description
            });
        }

        // PUT: api/categories/{id} (Update an existing category)
        [HttpPut("{id:int}")]
        public IActionResult UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = _dbContext.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            _dbContext.SaveChanges();

            return Ok(new
            {
                category.CategoryId,
                category.Name,
                category.Description
            });
        }

        // DELETE: api/categories/{id} (Delete a category)
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _dbContext.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            return Ok(new { message = "Category deleted successfully" });
        }
    }
}