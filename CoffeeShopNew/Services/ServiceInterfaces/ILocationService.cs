using CoffeeShopNew.Models;

namespace CoffeeShopNew.Services.ServiceInterfaces;

public interface ILocationService
{
    Task<List<Location>> GetAllLocations();
    Task<List<Location>> GetAllLocationsSolteredFiltered(string? filter, string? sortBy);
    Task<Location> GetLocationById(int id);
    Task<Location> CreateLocation(Location location);
    Task<Location> UpdateLocation(Location location);
    Task DeleteLocation(int id);
}