using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopNew.Models;

[Table("misc_items")]
public class MiscItem:Item
{
    public Image? Image { get; set; }
    public MiscItem()
    {
    }

    public MiscItem(string name, string? description, decimal price) : base(name, description, price)
    {
    }

    public MiscItem(int id, string name, string? description, decimal price) : base(id, name, description, price)
    {
    }
}