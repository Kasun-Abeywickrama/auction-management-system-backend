using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

[Route("api/categories/{categoryId}/images")]
[ApiController]
public class CategoryImagesController : ControllerBase
{
    private readonly CategoryImageService _imageService;

    public CategoryImagesController(CategoryImageService imageService)
    {
        _imageService = imageService;
    }

    // POST: api/categories/{categoryId}/images - Upload category images
    [HttpPost]
    public async Task<IActionResult> UploadImages(int categoryId, [FromForm] List<IFormFile> imageFiles)
    {
        if (!await _imageService.CheckCategoryExist(categoryId))
        {
            return NotFound("Category not found.");
        }

        if (imageFiles == null || imageFiles.Count == 0)
            return BadRequest("No image files provided.");

        var imageUrls = new List<string>();

        foreach (var imageFile in imageFiles)
        {
            var imageUrl = await _imageService.SaveCategoryImageAsync(categoryId, imageFile);
            imageUrls.Add(imageUrl);
        }

        // Save image URLs to the database
        var result = await _imageService.SaveImageUrlsToDatabaseAsync(categoryId, imageUrls);
        if (!result)
            return BadRequest("Failed to save image URLs to the database.");

        return Ok(new { imageUrls });
    }

    // GET: api/categories/{categoryId}/images - Retrieve category images
    [HttpGet]
    public IActionResult GetCategoryImages(int categoryId)
    {
        var categoryImagesFolder = Path.Combine("wwwroot", "category-images", categoryId.ToString());
        if (!Directory.Exists(categoryImagesFolder))
            return NotFound("No images found for this category.");

        var images = new List<string>();
        foreach (var imagePath in Directory.GetFiles(categoryImagesFolder))
        {
            var imageUrl = $"/category-images/{categoryId}/{Path.GetFileName(imagePath)}";
            images.Add(imageUrl);
        }

        return Ok(images);
    }

    // DELETE: api/categories/{categoryId}/images/{imageName} - Delete category image
    [HttpDelete("{imageName}")]
    public async Task<IActionResult> DeleteImage(int categoryId, string imageName)
    {
        if (!await _imageService.CheckCategoryExist(categoryId))
        {
            return NotFound("Category not found.");
        }

        var result = await _imageService.DeleteCategoryImageAsync(categoryId, imageName);
        if (!result)
            return NotFound("Image not found or could not be deleted.");

        // If the image was deleted successfully, remove the URL from the database
        var res = await _imageService.DeleteCategoryImageFromDBAsync(categoryId, imageName);
        if (!res)
            return BadRequest("Failed to delete image from the database.");

        return NoContent();
    }
}
