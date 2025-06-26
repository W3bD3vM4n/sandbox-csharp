using ApiLoopThree.Domain;

namespace ApiLoopThree.Services;

public interface IMeterReadingService
{
    List<ElectricityReading> GetReadings(string smartMeterId);
    void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
}