using ApiLoopThree.Domain;

namespace ApiLoopThree.Services;

public class MeterReadingService : IMeterReadingService
{
   // Comes from [4.0]
   // Map from smart‑meter IDs to their list of electricity readings
   public Dictionary<string, List<ElectricityReading>> MeterAssociatedReadings { get; set; }
   
   // Constructor injection to receive the dictionary
   // from the DI container (registered as a singleton earlier)
   public MeterReadingService(Dictionary<string, List<ElectricityReading>> meterAssociatedReadings)
   {
      MeterAssociatedReadings = meterAssociatedReadings;
   }

   // Comes from [9.1]
   // Method to get the meter readings
   public List<ElectricityReading> GetReadings(string smartMeterId)
   {
      // If contain the requested, returns the list of readings
      if (MeterAssociatedReadings.ContainsKey(smartMeterId))
      {
         return MeterAssociatedReadings[smartMeterId];
      }
      
      // If it doesn't, returns a new empty list
      return new List<ElectricityReading>();
   }

   // Comes from [9.2]
   // Method to save a new meter reading
   public void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings)
   {
      // If this meter ID is not already in your dictionary
      if (!MeterAssociatedReadings.ContainsKey(smartMeterId))
      {
         // Create a new key with an empty list as it's value
         MeterAssociatedReadings.Add(smartMeterId, new List<ElectricityReading>());
      }
      
      // Iterate over each electricityReading, while appending
      // to the existing list for that meter
      electricityReadings.ForEach(electricityReading => MeterAssociatedReadings[smartMeterId].Add(electricityReading));
   }
}