using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopNew.Models;

[Table("images")]
public class Image
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("path")]
    public string Path { get; set; }

    public Image()
    {
    }

    public Image(int id, string path)
    {
        Id = id;
        Path = path;
    }
}