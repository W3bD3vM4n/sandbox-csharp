using ApiLoopThree.Domain;

namespace ApiLoopThree.Services;

public class PricePlanService : IPricePlanService
{
    // A kind of log message (it’s never used in the rest of this service)
    public interface Debug
    {
        void Log(string s);
    }

    // Fields
    // Holds the available price plans (injected from Startup)
    private readonly List<PricePlan> _pricePlans;
    // Fetch meter readings for a given smart meter ID
    private IMeterReadingService _meterReadingService;

    // Constructor (doing DI)
    public PricePlanService(List<PricePlan> pricePlan, IMeterReadingService meterReadingService)
    {
        _pricePlans = pricePlan;
        _meterReadingService = meterReadingService;
    }

    // Math Operation #1
    private decimal calculateAverageReading(List<ElectricityReading> electricityReadings)
    {
        var newSummedReadings = electricityReadings
            // Extracts each reading value
            .Select(readings => readings.Reading)
            // Sums them with Aggregate
            .Aggregate((reading, accumulator) => reading + accumulator);

        // Divides by the count to get the average reading (units per reading interval)
        return newSummedReadings / electricityReadings.Count();
    }

    // Math Operation #2
    private decimal calculateTimeElapsed(List<ElectricityReading> electricityReadings)
    {
        // Finds the earliest timestamp
        var first = electricityReadings.Min(reading => reading.Time);
        // Finds the latest timestamp
        var last = electricityReadings.Max(reading => reading.Time);

        // Subtracts to get a TimeSpan to total hours
        return (decimal)(last - first).TotalHours;
    }

    // Math Operation #3
    private decimal calculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
    {
        // Compute average consumption per reading interval
        var average = calculateAverageReading(electricityReadings);
        var timeElapsed = calculateTimeElapsed(electricityReadings);
        // Divide to get the average consumption per hour
        var averagedCost = average / timeElapsed;

        // Multiply by the plan's unit rate (price per unit) to get a cost
        // and round to three decimal places
        return Math.Round(averagedCost * pricePlan.UnitRate, 3);
    }

    public Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId)
    {
        // Store the method outcome
        List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);

        if (!electricityReadings.Any())
        {
            // Return an empty dictionary, which means
            // "no readings, so no cost to compute"
            return new Dictionary<string, decimal>();
        }
        
        // Returns a dictionary mapping each plan’s name to its calculated cost
        // for the given meter’s readings
        return _pricePlans.ToDictionary(
            // For each PricePlan in _pricePlans, creates a key/value pair,
            plan => plan.PlanName,
            plan => calculateCost(electricityReadings, plan)
            );
    }
}