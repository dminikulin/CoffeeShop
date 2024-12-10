using CoffeeShopNew.Models;

namespace CoffeeShopNew.Services.ServiceInterfaces;

public interface ICoffeeItemService
{
    Task<List<CoffeeItem>> GetAllCoffeeItems();
    Task<List<CoffeeItem>> GetAllCoffeeItemsSortedFiltered(string? filter, string? sortBy);
    Task<CoffeeItem> GetCoffeeItemById(int id);
    Task<CoffeeItem> AddCoffeeItem(CoffeeItem coffeeItem, IFormFile? imageFile);
    Task<CoffeeItem> UpdateCoffeeItem(CoffeeItem coffeeItem, IFormFile? imageFile);
    Task DeleteCoffeeItem(int id);
}