using ApiLoopThree.Enums;

// 6. FOR OBJECT
namespace ApiLoopThree.Domain;

// [6.0] Class
public class PricePlan
{
    public string PlanName { get; set; }
    public Supplier EnergySupplier { get; set; }
    public decimal UnitRate { get; set; }
    // Comes from [6.1]
    // Uses IList to make the list flexible
    public IList<PeakTimeMultiplier> PeakTimeMultiplier { get; set; }
    
    // This is used for Testing
    public decimal GetPrice(DateTime dateTime)
    {
        // Comes from [6.1]
        // Searches PeakTimeMultiplier list for an entry that matches DayOfWeek
        var multiplier = PeakTimeMultiplier.FirstOrDefault(m => m.DayOfWeek == dateTime.DayOfWeek);

        // Calculates the price based on the base unit
        if (multiplier?.Multiplier != null)
        {
            return multiplier.Multiplier * UnitRate;
        }
        else
        {
            return UnitRate;
        }
    }
}

// [6.1] Class
public class PeakTimeMultiplier
{
    public DayOfWeek DayOfWeek { get; set; }
    public decimal Multiplier { get; set; }
}