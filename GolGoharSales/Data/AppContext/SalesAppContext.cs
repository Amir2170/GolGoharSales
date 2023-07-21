using Microsoft.EntityFrameworkCore;
using GolGoharSales.Models;

namespace GolGoharSales.Data.AppContext;

//Db Context for this app
public class SalesAppContext: DbContext
{
    public SalesAppContext(DbContextOptions<SalesAppContext> options)
        : base(options)
    {
    }
    
    //Warehouse model context Dbset
    public DbSet<Warehouse> Warehouses { get; set; }
    
    //Contract model context Dbset
    public DbSet<SalesContract> SalesContracts { get; set; }
    
    //Customer model context Dbset
    public DbSet<Customer> Customers { get; set; }
    
    //Location model context Dbset
    public DbSet<Location> Locations { get; set; }
    
    //Production model context Dbset
    public DbSet<Production> Productions { get; set; }
    
    //Transportation model context Dbset
    public DbSet<Transportation> Transportations { get; set; }
}