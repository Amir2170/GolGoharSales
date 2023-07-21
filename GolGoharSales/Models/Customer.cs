using System.ComponentModel.DataAnnotations;

namespace GolGoharSales.Models;

// Customer model with one to many relation to contract being as parent
public class Customer
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Address { get; set; }
    
    [DataType(DataType.PhoneNumber)]
    public string Telephone { get; set; }

    // collection navigation containing contracts
    public ICollection<SalesContract> Contracts { get; }= new List<SalesContract>();
}

