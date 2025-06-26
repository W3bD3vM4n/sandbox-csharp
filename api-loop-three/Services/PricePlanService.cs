using ApiLoopThree.Domain;

namespace ApiLoopThree.Services;

public class PricePlanService : IPricePlanService
{
    // What's the interface is used for?
    public interface Debug
    {
        void Log(string s);
    }

    // Fields
    private readonly List<PricePlan> _pricePlans;
    private IMeterReadingService _meterReadingService;

    // Constructor
    public PricePlanService(List<PricePlan> pricePlan, IMeterReadingService meterReadingService)
    {
        _pricePlans = pricePlan;
        _meterReadingService = meterReadingService;
    }

    // Math Operation #1
    private decimal calculateAverageReading(List<ElectricityReading> electricityReadings)
    {
        var newSummedReadings = electricityReadings.Select(readings => readings.Reading).Aggregate((reading, accumulator) => reading + accumulator);

        return newSummedReadings / electricityReadings.Count();
    }

    // Math Operation #2
    private decimal calculateTimeElapsed(List<ElectricityReading> electricityReadings)
    {
        var first = electricityReadings.Min(reading => reading.Time);
        var last = electricityReadings.Max(reading => reading.Time);

        // What's happening here?
        return (decimal)(last - first).TotalHours;
    }

    // Math Operation #3
    private decimal calculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
    {
        var average = calculateAverageReading(electricityReadings);
        var timeElapsed = calculateTimeElapsed(electricityReadings);
        var averagedCost = average / timeElapsed;

        return Math.Round(averagedCost * pricePlan.UnitRate, 3);
    }

    public Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId)
    {
        // A variable is storing a method outcome?
        List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);

        if (!electricityReadings.Any())
        {
            // What am I returning here?
            return new Dictionary<string, decimal>();
        }
        
        // Use of "Math Operation #3"
        return _pricePlans.ToDictionary(plan => plan.PlanName, plan => calculateCost(electricityReadings, plan));
    }
}