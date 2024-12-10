using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopNew.Models;

[Table("locations")]
public class Location
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    [Column("address")]
    public string Address { get; set; }
    [Required]
    [Column("weekday_hours")]
    public string WeekdayHours { get; set; }
    [Required]
    [Column("weekend_hours")]
    public string WeekendHours { get; set; }

    public Location()
    {
    }

    public Location(string address, string weekdayHours, string weekendHours)
    {
        Address = address;
        WeekdayHours = weekdayHours;
        WeekendHours = weekendHours;
    }

    public Location(int id, string address, string weekdayHours, string weekendHours)
    {
        Id = id;
        Address = address;
        WeekdayHours = weekdayHours;
        WeekendHours = weekendHours;
    }

    public string GetGoogleMapsUrl()
    {
        return $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(Address)}";
    }
}