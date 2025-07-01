using ApiLoopThree.Domain;

namespace ApiLoopThree.Generator;

public class ElectricityReadingGenerator
{
    // Simulates a list of ElectricityReading for a smart meter
    public List<ElectricityReading> Generate(int number)
    {
        // Initializes an empty list
        var readings = new List<ElectricityReading>();
        // Initializes random decimals
        var random = new Random();

        // Fill the data on Domain > ElectricityReading
        for (int i = 0; i < number; i++)
        {
            // Generate a random Reading, converting from
            // double to decimal (for more precision)
            var reading = (decimal)random.NextDouble();
            // Create an ElectricityReading object
            // joining Reading and Time
            var electricityReading = new ElectricityReading
            {
                Reading = reading,
                // Simulates readings taken every 10 seconds in the past
                // by subtracting to the current time
                Time = DateTime.Now.AddSeconds(-i * 10)
            };
            // Adds the single ElectricityReading to the list
            readings.Add(electricityReading);
        }
        // Sorts the list by Time in ascending order (oldest first)
        readings.Sort((reading1, reading2) => reading1.Time.CompareTo(reading2.Time));
        
        return readings;
    }
}