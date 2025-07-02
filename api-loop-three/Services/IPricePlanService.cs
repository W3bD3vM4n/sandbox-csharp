// 10. INTERFACE
namespace ApiLoopThree.Services;

// [10.0] For PricePlanService
public interface IPricePlanService
{
    Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId);
}