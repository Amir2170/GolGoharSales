using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GolGoharSales.Models;

// Contract model with one to many relation to Production being as child
// one to many relation to Customer being as child
// one to many relation to Transportation being as parent
public class SalesContract
{
    public int Id { get; set; }
    
    public int ContractNumber { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime ContractDate { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FinishDate { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    public int Monthly { get; set; }

    public int Value { get; set; }

    public int ProductionId { get; set; } // foreign key to Production

    public Production Production { get; set; } = null!; // navigation to Production
    
    public int CustomerId { get; set; } // foreign key to Customer

    public Customer Customer { get; set; } = null!; // navigation to Customer

    // collection navigation containing Transportations
    public ICollection<Transportation> Transportations { get; } = new List<Transportation>();
}

// SalesContract Model without navigation fields
public class SaleContractDTO
{
    public int Id { get; set; }
    
    public int ContractNumber { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime ContractDate { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FinishDate { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    public int Monthly { get; set; }

    public int Value { get; set; }

    public int ProductionId { get; set; } // foreign key to Production
    
    public int CustomerId { get; set; } // foreign key to Customer
}