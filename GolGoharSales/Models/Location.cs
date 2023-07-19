using System.ComponentModel.DataAnnotations;

namespace GolGoharSales.Models;

//Location model with one to many realtion to Warehouse model being as parent
public class Location
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    // Collection navigation containing warehouse models
    public ICollection<Warehouse> Warehouses { get; } = new List<Warehouse>();
}