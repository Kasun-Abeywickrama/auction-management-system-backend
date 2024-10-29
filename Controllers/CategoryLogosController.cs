using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AuctionManagementAPI.Controllers
{
   
    [Route("api/categories/{categoryId}/logos")]
    [ApiController]
    public class CategoryLogosController : ControllerBase
    {
        private readonly CategoryImageService _imageService;

        public CategoryLogosController(CategoryImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadLogos(int categoryId, [FromForm] List<IFormFile> logoFiles)
        {
            if (!await _imageService.CheckCategoryExist(categoryId))
            {
                return NotFound("Category not found.");
            }

            if (logoFiles == null || logoFiles.Count == 0)
                return BadRequest("No logo files provided.");

            var logoUrls = new List<string>();

            foreach (var logoFile in logoFiles)
            {
                var logoUrl = await _imageService.SaveCategoryLogoAsync(categoryId, logoFile);
                logoUrls.Add(logoUrl);
            }

            // Save logo URLs to the database
            var result = await _imageService.SaveLogoUrlsToDatabaseAsync(categoryId, logoUrls);
            if (!result)
                return BadRequest("Failed to save logo URLs to the database.");

            return Ok(new { logoUrls });
        }

        [HttpGet]
        public IActionResult GetCategoryLogos(int categoryId)
        {
            var logoFolder = Path.Combine("wwwroot", "category-logos", categoryId.ToString());
            if (!Directory.Exists(logoFolder))
                return NotFound("No logos found for this category.");

            var logos = new List<string>();
            foreach (var logoPath in Directory.GetFiles(logoFolder))
            {
                var logoUrl = $"/category-logos/{categoryId}/{Path.GetFileName(logoPath)}";
                logos.Add(logoUrl);
            }

            return Ok(logos);
        }

        [HttpDelete("{logoName}")]
        public async Task<IActionResult> DeleteLogo(int categoryId, string logoName)
        {
            if (!await _imageService.CheckCategoryExist(categoryId))
            {
                return NotFound("Category not found.");
            }

            var result = await _imageService.DeleteCategoryLogoAsync(categoryId, logoName);
            if (!result)
                return NotFound("Logo not found or could not be deleted.");

            // If the logo was deleted successfully, remove the URL from the database
            var res = await _imageService.DeleteCategoryLogoFromDBAsync(categoryId, logoName);
            if (!res)
                return BadRequest("Failed to delete logo from the database.");

            return NoContent();
        }
    }

}
