using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public class ProductImageService
{
    private readonly IWebHostEnvironment _environment;

    public ProductImageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveProductImageAsync(int productId, IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            throw new ArgumentException("Invalid image file.");

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "product-images", productId.ToString());
        Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        // Return the relative URL to the saved image
        return $"/product-images/{productId}/{uniqueFileName}";
    }



    public async Task<bool> DeleteProductImageAsync(int productId, string imageName)
    {
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "product-images", productId.ToString());
        var filePath = Path.Combine(uploadsFolder, imageName);

        if (!File.Exists(filePath))
            return false;

        try
        {
            File.Delete(filePath);
            return true;
        }
        catch
        {
            return false;
        }
    }




}
