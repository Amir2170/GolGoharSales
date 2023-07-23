using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.WarehouseRepositoryNS;

public class WarehouseRepository : GenericRepository<Warehouse> , IWarehouseRepository
{
    // supplying base class with injected context
    public WarehouseRepository(SalesAppContext context) : base(context)
    {
    }
    
    /* check if a warehouse by the same title already exists
      we can not check by location because a location contains 
      several warehouses */
    public bool WarehouseExists(Warehouse warehouse)
    {
        return Context.Warehouses.Any(dbWarehouse =>
            dbWarehouse.Title == warehouse.Title);
    }
}