using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GolGoharSales.Models;

// Warehouse model with one to many relation to location being as child
// one to many relation to Production being as parent
public class Warehouse
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public int LocationId { get; set; } //required foreign key of location model

    public Location Location { get; set; } = null!; // required navigation to location model
    
    // collection navigation containing productions
    public ICollection<Production> Productions { get; } = new List<Production>();
}

// model view without navigation property
public class WarehouseDTO
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public int LocationId { get; set; } //required foreign key of location model
}