using AuctionManagementAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public class ProductImageService
{
    private readonly IWebHostEnvironment _environment;

    private readonly AuctionContext _context;

    public ProductImageService(AuctionContext _context, IWebHostEnvironment environment)
    {
        _environment = environment;
        this._context = _context;
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



    public async Task<bool> SaveImageUrlsToDatabaseAsync(int productId, List<string> imageUrls)
    {

        // Add new the image URLs also into the product table in the database
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
            return false;

        if (product.ImageUrls == null)
            product.ImageUrls = new List<string>();

        product.ImageUrls.AddRange(imageUrls);
        await _context.SaveChangesAsync();
        return true;

    }


    // function to Check weather there is a product for this productId
    public async Task<bool> CheckProductExist(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
            return false;
        return true;
    }


    public async Task<bool> DeleteProductImageFromDBAsync(int productId, string imageName)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
            return false;

        if (product.ImageUrls == null || product.ImageUrls.Count == 0)
            return false;

        // create image url from image name
        var imageUrls = $"/product-images/{productId}/{imageName}";

        if (!product.ImageUrls.Contains(imageUrls))
            return false;

        product.ImageUrls.Remove(imageUrls);
        await _context.SaveChangesAsync();
        return true;
    }

}
