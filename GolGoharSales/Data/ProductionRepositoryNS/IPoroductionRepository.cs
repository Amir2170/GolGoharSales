using GolGoharSales.Data.GenericRepositoryNS;
using GolGoharSales.Models;

namespace GolGoharSales.Data.ProductionRepositoryNS;

public interface IPoroductionRepository : IGenericRepository<Production>
{
    //check if production exists
    bool ProductionExists(Production production);
}