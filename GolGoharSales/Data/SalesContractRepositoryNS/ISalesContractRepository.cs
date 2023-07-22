using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.SalesContractRepositoryNS;

public interface ISalesContractRepository : IGenericRepository<SalesContract>
{
    // check if contracts already exists in database
    bool ContractExists(SalesContract contract);
}