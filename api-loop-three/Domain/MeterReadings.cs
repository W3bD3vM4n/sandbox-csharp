// 5. FOR OBJECT
namespace ApiLoopThree.Domain;

// [5.0] Class
public class MeterReadings
{
    public string SmartMeterId { get; set; }
    // Comes from [4.0]
    public List<ElectricityReading> ElectricityReadings { get; set; }
}