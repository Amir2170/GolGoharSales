using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.WarehouseRepositoryNS;

public interface IWarehouseRepository : IGenericRepository<Warehouse>
{
    // check if warehouse exists
    bool WarehouseExists(Warehouse warehouse);
}