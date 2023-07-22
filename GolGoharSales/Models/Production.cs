using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GolGoharSales.Models;

// Production model with one to many relation to Warehouse being as child
// one to many relation to Contract being as parent
public class Production
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    public int StrategicResource { get; set; }
    
    public string Code { get; set; }
    
    public int WarehouseId { get; set; } // required foreign key to warehouse

    public Warehouse Warehouse { get; set; } = null!; // required navigation to warehouse 

    // collection navigation containing contracts
    public ICollection<SalesContract> Contracts { get; } = new List<SalesContract>();
}