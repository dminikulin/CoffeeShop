using CoffeeShopNew.Context;
using CoffeeShopNew.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopNew.Services.ServiceInterfaces;

public class MiscItemService:IMiscItemService
{
    private readonly CoffeeShopContext _context;
    private readonly ILogger<CoffeeItemService> _logger;
    private readonly IImageService _imageService;

    public MiscItemService(CoffeeShopContext context, ILogger<CoffeeItemService> logger, IImageService imageService)
    {
        _context = context;
        _logger = logger;
        _imageService = imageService;
    }

    public async Task<List<MiscItem>> GetAllItems()
    {
        return await _context.MiscItems.Include(m => m.Image).ToListAsync();
    }

    public async Task<List<MiscItem>> GetAllItemsSortedFiltered(string? filter, string? sortBy)
    {
        var query = _context.MiscItems.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(item => item.Name.ToLower().Contains(filter.ToLower()));
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

    public async Task<MiscItem> GetItemById(int id)
    {
        var miscItem = await _context.MiscItems.Include(m => m.Image).SingleOrDefaultAsync(m => m.Id == id);
        if (miscItem == null)
        {
            _logger.LogInformation("Item with id {id} not found", id);
            throw new KeyNotFoundException("Item not found");
        }
        return miscItem;
    }

    public async Task<MiscItem> AddItem(MiscItem miscItem, IFormFile? imageFile)
    {
        if(miscItem == null) throw new ArgumentNullException("Item is empty");
        
        if (imageFile != null)
        {
            var imageId = await _imageService.UploadImageAsync(imageFile, "MiscItems");
            miscItem.ImageId = imageId; // Associate the image with the misc item
        }
        
        _context.MiscItems.Add(miscItem);
        await _context.SaveChangesAsync();
        return miscItem;
    }

    public async Task<MiscItem> UpdateItem(MiscItem miscItem, IFormFile? imageFile)
    {
        if(miscItem == null) throw new ArgumentNullException("Item is empty");
        
        if (imageFile != null)
        {
            var imageId = await _imageService.UploadImageAsync(imageFile, "MiscItems");
            miscItem.ImageId = imageId; // Associate the new image with the misc item
        }
        
        _context.Entry(miscItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return miscItem;
    }

    public async Task DeleteItem(int id)
    {
        var miscItem = await _context.MiscItems.FindAsync(id);
        if (miscItem == null)
        {
            _logger.LogInformation("Item with id {id} not found", id);
            throw new KeyNotFoundException("Item not found");
        }

        if (miscItem.ImageId.HasValue)
        {
            await _imageService.DeleteImageAsync(miscItem.ImageId.Value, "MiscItems");
        }
        _context.MiscItems.Remove(miscItem);
        await _context.SaveChangesAsync();
    }
}