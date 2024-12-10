using CoffeeShopNew.Models;

namespace CoffeeShopNew.Services.ServiceInterfaces;

public interface IMiscItemService
{
    Task<List<MiscItem>> GetAllItems();
    Task<List<MiscItem>> GetAllItemsSortedFiltered(string? filter, string? sortBy);
    Task<MiscItem> GetItemById(int id);
    Task<MiscItem> AddItem(MiscItem miscItem, IFormFile? imageFile);
    Task<MiscItem> UpdateItem(MiscItem miscItem, IFormFile? imageFile);
    Task DeleteItem(int id);
}