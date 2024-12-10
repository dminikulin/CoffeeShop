using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace CoffeeShopNew.Models;

[Table("coffee_items")]
public class CoffeeItem:Item
{
    [Column("country")]
    public string Country { get; set; }
    
    public Image? Image { get; set; }

    public CoffeeItem()
    {
    }

    public CoffeeItem(string name, string? description, decimal price, string country) : base(name, description, price)
    {
        Country = country;
    }

    public CoffeeItem(int id, string name, string? description, decimal price, string country) : base(id, name, description, price)
    {
        Country = country;
    }

    public string CountryCode()
    {
        var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(c => new RegionInfo(c.Name)).ToList();
        var countryRegion = regions.FirstOrDefault(r => r.EnglishName.Equals(Country, StringComparison.OrdinalIgnoreCase));
        return countryRegion.ThreeLetterWindowsRegionName;
    }
}