using ApiLoopThree.Domain;

// 9. INTERFACE
namespace ApiLoopThree.Services;

// [9.0] For MeterReadingService
public interface IMeterReadingService
{
    // [9.1] Property
    List<ElectricityReading> GetReadings(string smartMeterId);
    // [9.2] Property
    void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
}