using CoffeeShopNew.Context;
using CoffeeShopNew.Models;
using CoffeeShopNew.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopNew.Services;

public class LocationService: ILocationService
{
    private readonly CoffeeShopContext _context;
    private readonly ILogger<CoffeeItemService> _logger;

    public LocationService(CoffeeShopContext context, ILogger<CoffeeItemService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Location>> GetAllLocations()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<List<Location>> GetAllLocationsSolteredFiltered(string? filter, string? sortBy)
    {
        var query = _context.Locations.AsQueryable();
        
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(item => item.Address.ToLower().Contains(filter.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy)
            {
                case "AddressAsc":
                    query = query.OrderBy(c => c.Address);
                    break;
                case "AddressDesc":
                    query = query.OrderByDescending(c => c.Address);
                    break;
                default:
                    break;
            }
        }
        
        return await query.ToListAsync();
    }

    public async Task<Location> GetLocationById(int id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null)
        {
            _logger.LogInformation("Location with id {id} not found", id);
            throw new KeyNotFoundException("Location not found");
        }
        return location;
    }

    public async Task<Location> CreateLocation(Location location)
    {
        if(location == null) throw new ArgumentNullException("Location is empty");
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        return location;
    }

    public async Task<Location> UpdateLocation(Location location)
    {
        if(location == null) throw new ArgumentNullException("Location is empty");
        _context.Entry(location).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return location;
    }

    public async Task DeleteLocation(int id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null)
        {
            _logger.LogError($"Location with id {id} not found");
            throw new KeyNotFoundException("Location not found");
        }
        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
    }
}