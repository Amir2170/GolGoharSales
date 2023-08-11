using System;
using System.ComponentModel.DataAnnotations;

namespace GolGoharSales.Models;

// Transportation model with one to many relation to Contract being as child
public class Transportation
{
    public int Id { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }

    public int SetTonnage { get; set; }
    
    public int SalesContractId { get; set; } // foreing key to Contract

    public SalesContract SalesContracts { get; set; } = null!; // navigation to Contract
}

// transportation model view without navigation fields
public class TransportationDTO
{
    public int Id { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }

    public int SetTonnage { get; set; }
    
    public int SalesContractId { get; set; } // foreing key to Contract
}