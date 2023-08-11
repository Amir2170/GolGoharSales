using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
    public ICollection<SalesContract> SalesContracts { get; }= new List<SalesContract>();
}

// customer DTO without salesContracts navigation to use in frontend
public class CustomerDTO
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Address { get; set; }
    
    [DataType(DataType.PhoneNumber)]
    public string Telephone { get; set; }
}