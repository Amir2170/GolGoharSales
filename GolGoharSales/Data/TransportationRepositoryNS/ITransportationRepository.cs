using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.TransportationRepositoryNS;

public interface ITransportationRepository : IGenericRepository<Transportation>
{
    // no need for checking transportation existence
}