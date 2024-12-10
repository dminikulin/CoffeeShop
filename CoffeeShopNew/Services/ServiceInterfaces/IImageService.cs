namespace CoffeeShopNew.Services.ServiceInterfaces;

public interface IImageService
{
    Task<int> UploadImageAsync(IFormFile file, string itemType);
    Task DeleteImageAsync(int imageId, string itemType);

}