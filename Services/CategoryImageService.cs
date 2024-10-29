﻿using AuctionManagementAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class CategoryImageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly AuctionContext _context;

    public CategoryImageService(AuctionContext context, IWebHostEnvironment environment)
    {
        _environment = environment;
        _context = context;
    }

    // Save category image to the file system and return the URL
    public async Task<string> SaveCategoryImageAsync(int categoryId, IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            throw new ArgumentException("Invalid image file.");

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "category-images", categoryId.ToString());
        Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        return $"/category-images/{categoryId}/{uniqueFileName}";
    }

    // Delete category image from the file system
    public async Task<bool> DeleteCategoryImageAsync(int categoryId, string imageName)
    {
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "category-images", categoryId.ToString());
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

    // Save category image URLs to the database
    public async Task<bool> SaveImageUrlsToDatabaseAsync(int categoryId, List<string> imageUrls)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category == null)
            return false;

        if (category.ImageUrls == null)
            category.ImageUrls = new List<string>();

        category.ImageUrls.AddRange(imageUrls);
        await _context.SaveChangesAsync();
        return true;
    }

    // Check if category exists in the database
    public async Task<bool> CheckCategoryExist(int categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        return category != null;
    }

    // Delete image URL from the database
    public async Task<bool> DeleteCategoryImageFromDBAsync(int categoryId, string imageName)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category == null || category.ImageUrls == null || category.ImageUrls.Count == 0)
            return false;

        var imageUrl = $"/category-images/{categoryId}/{imageName}";

        if (!category.ImageUrls.Contains(imageUrl))
            return false;

        category.ImageUrls.Remove(imageUrl);
        await _context.SaveChangesAsync();
        return true;
    }
}