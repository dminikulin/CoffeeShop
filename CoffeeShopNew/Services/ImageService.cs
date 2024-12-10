using CoffeeShopNew.Context;
using CoffeeShopNew.Models;
using CoffeeShopNew.Services.ServiceInterfaces;

namespace CoffeeShopNew.Services;

public class ImageService:IImageService
{
    private readonly CoffeeShopContext _context;
    private readonly string _coffeeImageDirectory;
    private readonly string _miscImageDirectory;

    public ImageService(CoffeeShopContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _coffeeImageDirectory = Path.Combine(hostingEnvironment.WebRootPath, "images", "CoffeeItems");
        _miscImageDirectory = Path.Combine(hostingEnvironment.WebRootPath, "images", "MiscItems");
        
        Directory.CreateDirectory(_coffeeImageDirectory);
        Directory.CreateDirectory(_miscImageDirectory);
    }

    public async Task<int> UploadImageAsync(IFormFile file, string itemType)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file uploaded.");

        // Determine folder based on item type
        string folderPath = itemType == "CoffeeItems" ? _coffeeImageDirectory : _miscImageDirectory;
            
        // Generate a unique filename
        string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
        string filePath = Path.Combine(folderPath, fileName);

        // Save the file to the server
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Save the image path in the database
        var image = new Image
        {
            Path = Path.Combine("images", itemType, fileName)
        };

        _context.Images.Add(image);
        await _context.SaveChangesAsync();

        return image.Id;
    }

    public async Task DeleteImageAsync(int imageId, string itemType)
    {
        var image = await _context.Images.FindAsync(imageId);
        if (image != null)
        {
            // Delete the image file from the server
            string folderPath = itemType == "CoffeeItems" ? _coffeeImageDirectory : _miscImageDirectory;
            string filePath = Path.Combine(folderPath, Path.GetFileName(image.Path));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Remove the image record from the database
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}