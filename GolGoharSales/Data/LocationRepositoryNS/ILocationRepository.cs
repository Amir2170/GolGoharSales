using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.LocationRepositoryNS;

public interface ILocationRepository : IGenericRepository<Location>
{
    // check if location exists
    bool LocationExists(Location location);
}