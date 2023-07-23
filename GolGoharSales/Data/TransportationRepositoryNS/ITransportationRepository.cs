using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.TransportationRepositoryNS;

public interface ITransportationRepository : IGenericRepository<Transportation>
{
    // check if transportation already exists
    bool TransportationExists(Transportation transportation);
}