using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopNew.Models;

public abstract class Item
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Required]
    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
    
    [ForeignKey("image_id")]
    [Column("image_id")]
    public int? ImageId { get; set; }

    protected Item()
    {
    }

    protected Item(string name, string? description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    protected Item(string name, string? description, decimal price, int? imageId)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageId = imageId;
    }

    protected Item(int id, string name, string? description, decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
    }

    protected Item(int id, string name, string? description, decimal price, int? imageId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        ImageId = imageId;
    }
}