using CoffeeShopNew.Context;
using CoffeeShopNew.Models;
using CoffeeShopNew.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopNew.Services;

public class CoffeeItemService: ICoffeeItemService
{

    private readonly CoffeeShopContext _context;
    private readonly ILogger<CoffeeItemService> _logger;
    private readonly IImageService _imageService;

    public CoffeeItemService(CoffeeShopContext context, ILogger<CoffeeItemService> logger, IImageService imageService)
    {
        _context = context;
        _logger = logger;
        _imageService = imageService;
    }

    public async Task<List<CoffeeItem>> GetAllCoffeeItems()
    {
        return await _context.CoffeeItems.Include(c => c.Image).ToListAsync();
    }

    public async Task<List<CoffeeItem>> GetAllCoffeeItemsSortedFiltered(string? filter, string? sortBy)
    {
        var query = _context.CoffeeItems.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(item => item.Name.ToLower().Contains(filter.ToLower()) || item.Country.ToLower().Contains(filter.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy)
            {
                case "NameAsc":
                    query = query.OrderBy(c => c.Name);
                    break;
                case "NameDesc":
                    query = query.OrderByDescending(c => c.Name);
                    break;
                case "PriceAsc":
                    query = query.OrderBy(c => c.Price);
                    break;
                case "PriceDesc":
                    query = query.OrderByDescending(c => c.Price);
                    break;
                default:
                    break;
            }
        }
        
        return await query.ToListAsync();
    }
    public async Task<CoffeeItem> GetCoffeeItemById(int id)
    {
        var coffeeItem = await _context.CoffeeItems.Include(c => c.Image).SingleOrDefaultAsync(c => c.Id == id);
        if (coffeeItem == null)
        {
            _logger.LogWarning("Coffee item with ID {Id} not found", id);
            throw new KeyNotFoundException("Coffee item not found");
        }
        return coffeeItem;
    }

    public async Task<CoffeeItem> AddCoffeeItem(CoffeeItem coffeeItem, IFormFile? imageFile)
    {
        if (coffeeItem == null) throw new ArgumentNullException("Coffee item is empty");
        
        if (imageFile != null)
        {
            var imageId = await _imageService.UploadImageAsync(imageFile, "CoffeeItems");
            coffeeItem.ImageId = imageId; // Associate the image with the coffee item
        }
        
        _context.CoffeeItems.Add(coffeeItem);
        await _context.SaveChangesAsync();
        return coffeeItem;
    }

    public async Task<CoffeeItem> UpdateCoffeeItem(CoffeeItem coffeeItem, IFormFile? imageFile)
    {
        if (coffeeItem == null) throw new ArgumentNullException("Coffee item is empty");
        
        if (imageFile != null)
        {
            var imageId = await _imageService.UploadImageAsync(imageFile, "CoffeeItems");
            coffeeItem.ImageId = imageId; // Associate the new image with the coffee item
        }
        
        _context.Entry(coffeeItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return coffeeItem;
    }

    public async Task DeleteCoffeeItem(int id)
    {
        var coffeeItem = await _context.CoffeeItems.FindAsync(id);
        if (coffeeItem == null)
        {
            _logger.LogWarning("Coffee item with ID {Id} not found", id);
            throw new KeyNotFoundException("Coffee item not found");
        }
        if (coffeeItem.ImageId.HasValue)
        {
            await _imageService.DeleteImageAsync(coffeeItem.ImageId.Value, "CoffeeItems");
        }
        _context.CoffeeItems.Remove(coffeeItem);
        await _context.SaveChangesAsync();
    }
}