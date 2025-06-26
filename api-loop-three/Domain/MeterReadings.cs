namespace ApiLoopThree.Domain;

public class MeterReadings
{
    public string SmartMeterId { get; set; }
    public List<ElectricityReading> ElectricityReadings { get; set; }
}