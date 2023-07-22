using System.Linq;
using GolGoharSales.Data.AppContext;
using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.SalesContractRepositoryNS;

public class SalesContractRepository : GenericRepository<SalesContract> , ISalesContractRepository
{
    // injecting context into base class
    public SalesContractRepository(SalesAppContext context) : base(context)
    {
    }
    
    /* check if customer with given ContractNumber exists
      ids are always different in database */
    public bool ContractExists(SalesContract contract)
    {
        return Context.SalesContracts.Any(dbContract =>
            dbContract.ContractNumber == contract.ContractNumber);
    }
}