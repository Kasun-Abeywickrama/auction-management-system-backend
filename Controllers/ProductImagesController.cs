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
        if (imageFiles == null || imageFiles.Count == 0)
            return BadRequest("No image files provided.");

        var imageUrls = new List<string>();

        foreach (var imageFile in imageFiles)
        {
            var imageUrl = await _imageService.SaveProductImageAsync(productId, imageFile);
            imageUrls.Add(imageUrl);
        }

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
        var result = await _imageService.DeleteProductImageAsync(productId, imageName);

        if (!result)
            return NotFound("Image not found or could not be deleted.");

        return NoContent();
    }


}
