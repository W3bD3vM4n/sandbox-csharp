using ApiLoopThree.Domain;

namespace ApiLoopThree.Services;

public class MeterReadingService : IMeterReadingService
{
   // A dictionary is defined to store the data
   public Dictionary<string, List<ElectricityReading>> MeterAssociatedReadings { get; set; }
   
   // The constructor passes the data to the dictionary
   public MeterReadingService(Dictionary<string, List<ElectricityReading>> meterAssociatedReadings)
   {
      MeterAssociatedReadings = meterAssociatedReadings;
   }

   // Method to get the meter readings
   public List<ElectricityReading> GetReadings(string smartMeterId)
   {
      if (MeterAssociatedReadings.ContainsKey(smartMeterId))
      {
         return MeterAssociatedReadings[smartMeterId];
      }
      
      // What am I returning here?
      return new List<ElectricityReading>();
   }

   // Method to save a new meter reading
   public void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings)
   {
      if (!MeterAssociatedReadings.ContainsKey(smartMeterId))
      {
         // How does the line save the data?
         MeterAssociatedReadings.Add(smartMeterId, new List<ElectricityReading>());
      }
      
      // What's happening in this step?
      electricityReadings.ForEach(electricityReading => MeterAssociatedReadings[smartMeterId].Add(electricityReading));
   }
}