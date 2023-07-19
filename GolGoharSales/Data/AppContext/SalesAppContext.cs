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
    
    //Warehouse model context
    public DbSet<Warehouse> Warehouses { get; set; }
}