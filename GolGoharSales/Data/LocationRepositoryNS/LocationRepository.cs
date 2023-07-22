using System.Linq;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.LocationRepositoryNS;

public class LocationRepository : GenericRepository<Location> , ILocationRepository
{
    // supplying injected context to base class
    public LocationRepository(SalesAppContext context) : base(context)
    {
    }
    
    /* check if location with given title exists
      ids are always different in database */
    public bool LocationExists(Location location)
    {
        return Context.Locations.Any(dbLocation =>
            dbLocation.Title == location.Title);
    }
}