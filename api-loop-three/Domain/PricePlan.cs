using ApiLoopThree.Enums;

namespace ApiLoopThree.Domain;

public class PricePlan
{
    public string PlanName { get; set; }
    public Supplier EnergySupplier { get; set; }
    public decimal UnitRate { get; set; }
    // Why was it used IList instead of just List?
    public IList<PeakTimeMultiplier> PeakTimeMultiplier { get; set; }
    
    // This is used for Testing
    public decimal GetPrice(DateTime dateTime)
    {
        var multiplier = PeakTimeMultiplier.FirstOrDefault(m => m.DayOfWeek == dateTime.DayOfWeek);

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

public class PeakTimeMultiplier
{
    public DayOfWeek DayOfWeek { get; set; }
    public decimal Multiplier { get; set; }
}