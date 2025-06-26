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
    public PricePlanComparatorController(IPricePlanService pricePlanService, IAccountService accountService)
    {
        this._pricePlanService = pricePlanService;
        this._accountService = accountService;
    }

    [HttpGet("compare-all/{smartMeterId}")]
    public ObjectResult CalculatedCostForEachPricePlan(string smartMeterId)
    {
        // I'm calling Services' methods
        string pricePlanId = _accountService.GetPricePlanIdForSmartMeterId(smartMeterId);
        Dictionary<string, decimal> costPerPricePlan = _pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);

        if (!costPerPricePlan.Any())
        {
            return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
        }

        // What kind of return is this?
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

        var recommendations = consumptionForPricePlans.OrderBy(pricePlanComparison => pricePlanComparison.Value);

        // 2nd option of return
        if (limit.HasValue && limit.Value < recommendations.Count())
        {
            return new ObjectResult(recommendations.Take(limit.Value));
        }

        /* 3rd option of return
           What kind of return is this? */
        return new ObjectResult(recommendations);
    }
}