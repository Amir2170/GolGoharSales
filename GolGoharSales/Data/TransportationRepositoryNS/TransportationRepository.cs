using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.TransportationRepositoryNS;

public class TransportationRepository : GenericRepository<Transportation> , ITransportationRepository
{
    public TransportationRepository(SalesAppContext context) : base(context)
    {
    }
    
    // no need for checking transportation existence
}