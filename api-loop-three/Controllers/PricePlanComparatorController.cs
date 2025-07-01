using ApiLoopThree.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoopThree.Controllers;

[Route("price-plans")]
public class PricePlanComparatorController : Controller
{
    // Fields
    public const string PRICE_PLAN_ID_KEY = "pricePlanId";
    public const string PRICE_PLAN_COMPARISONS_KEY = "pricePlanComparisons";
    // DI from the Services' Interface
    private readonly IPricePlanService _pricePlanService;
    private readonly IAccountService _accountService;

    // Constructor
    // The services are passed in and assigned to the private fields
    public PricePlanComparatorController(IPricePlanService pricePlanService, IAccountService accountService)
    {
        // This keeps your controller thin,
        // delegating business logic to the services
        this._pricePlanService = pricePlanService;
        this._accountService = accountService;
    }

    [HttpGet("compare-all/{smartMeterId}")]
    public ObjectResult CalculatedCostForEachPricePlan(string smartMeterId)
    {
        // I'm calling Services' methods
        string pricePlanId = _accountService.GetPricePlanIdForSmartMeterId(smartMeterId);
        Dictionary<string, decimal> costPerPricePlan = _pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);

        // If there are no readings (or no such meter),
        // it returns a 404 Not Found with a message
        if (!costPerPricePlan.Any())
        {
            return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
        }

        // And IActionResult that by default produces a 200 OK response
        // with the object you pass it serialized as JSON
        return new ObjectResult(new Dictionary<string, object>()
        {
            { PRICE_PLAN_ID_KEY, costPerPricePlan },
            { PRICE_PLAN_COMPARISONS_KEY, costPerPricePlan }
        });
    }

    [HttpGet("recommend/{smartMeterId}")]
    public ObjectResult RecommendCheapestPricePlans(string smartMeterId, int? limit = null)
    {
        var consumptionForPricePlans =
            _pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);

        // 1st option of return
        if (!consumptionForPricePlans.Any())
        {
            return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
        }

        // Produces listing plans from cheapest to priciest
        var recommendations = consumptionForPricePlans.OrderBy(pricePlanComparison => pricePlanComparison.Value);

        // 2nd option of return
        // If the specified limit it's less than the total number of plans
        if (limit.HasValue && limit.Value < recommendations.Count())
        {
            // Return only the top N "cheapest" plans
            return new ObjectResult(recommendations.Take(limit.Value));
        }

        // 3rd option of return
        // Sends a 200 OK with the serialized object
        return new ObjectResult(recommendations);
    }
}