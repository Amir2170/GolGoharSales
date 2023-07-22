using System;
using System.Linq;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.ProductionRepositoryNS;

public class ProductionRepository : GenericRepository<Production> , IPoroductionRepository
{
    public ProductionRepository(SalesAppContext context) : base(context)
    {
    }
    
    /* check if production with given title , strategicCode , code , warehouseId exists
      ids are always different in database */
    public bool ProductionExists(Production production)
    {
        return Context.Productions.Any(dbProduction =>
            dbProduction.Title == production.Title &&
            dbProduction.StrategicResource == production.StrategicResource &&
            dbProduction.Code == production.Code &&
            dbProduction.WarehouseId == production.WarehouseId);
    }
}