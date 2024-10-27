using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

[Route("api/products/{productId}/images")]
[ApiController]
public class ProductImagesController : ControllerBase
{
    private readonly ProductImageService _imageService;

    public ProductImagesController(ProductImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImages(int productId, [FromForm] List<IFormFile> imageFiles)
    {
        if (!await _imageService.CheckProductExist(productId)) {  
            return NotFound("Product not found.");
        }
       

        if (imageFiles == null || imageFiles.Count == 0)
            return BadRequest("No image files provided.");

        var imageUrls = new List<string>();

        foreach (var imageFile in imageFiles)
        {
            var imageUrl = await _imageService.SaveProductImageAsync(productId, imageFile);
            imageUrls.Add(imageUrl);
        }

        // Save image URLs to the database
        var result = await _imageService.SaveImageUrlsToDatabaseAsync(productId, imageUrls);
        if (!result)
            return BadRequest("Failed to save image URLs to the database.");


        return Ok(new { imageUrls });
    }



    [HttpGet]
    public IActionResult GetProductImages(int productId)
    {

        var productImagesFolder = Path.Combine("wwwroot", "product-images", productId.ToString());
        if (!Directory.Exists(productImagesFolder))
            return NotFound("No images found for this product.");

        var images = new List<string>();
        foreach (var imagePath in Directory.GetFiles(productImagesFolder))
        {
            // Use Path.GetFileName to get the file name and construct the URL using forward slashes
            var imageUrl = $"/product-images/{productId}/{Path.GetFileName(imagePath)}";
            images.Add(imageUrl);
        }

        return Ok(images);
    }



    [HttpDelete("{imageName}")]
    public async Task<IActionResult> DeleteImage(int productId, string imageName)
    {

        if (!await _imageService.CheckProductExist(productId))
        {
            return NotFound("Product not found.");
        }

        var result = await _imageService.DeleteProductImageAsync(productId, imageName);
        if (!result)
            return NotFound("Image not found or could not be deleted.");

        // If the image was deleted successfully, remove the URL from the database
        var res = await _imageService.DeleteProductImageFromDBAsync(productId, imageName);
        if (!res)
            return BadRequest("Failed to delete image from the database.");

        return NoContent();
    }


}
