using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.TransportationRepositoryNS;

public class TransportationRepository : GenericRepository<Transportation> , ITransportationRepository
{
    public TransportationRepository(SalesAppContext context) : base(context)
    {
    }
    
    /* check if transportation with given date , tonnage , SalesContract exists
    ids are always different in database */
    public bool TransportationExists(Transportation transportation)
    {
        return Context.Transportations.Any(dbTransportation =>
            dbTransportation.Date == transportation.Date &&
            dbTransportation.SalesContractId == transportation.SalesContractId &&
            dbTransportation.SetTonnage == transportation.SetTonnage);
    }
}