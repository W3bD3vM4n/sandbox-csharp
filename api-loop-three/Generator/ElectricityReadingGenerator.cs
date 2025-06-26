using ApiLoopThree.Domain;

namespace ApiLoopThree.Generator;

public class ElectricityReadingGenerator
{
    // Method (what's the logic behind it?)
    public List<ElectricityReading> Generate(int number)
    {
        // Initialize a variable for list
        var readings = new List<ElectricityReading>();
        // Initialize a variable for random value
        var random = new Random();

        // I'm filling the ElectricityReading Domain's data?
        for (int i = 0; i < number; i++)
        {
            // What's happening here?
            var reading = (decimal)random.NextDouble();
            // Assign the values to the fields
            var electricityReading = new ElectricityReading
            {
                Reading = reading,
                // What's the logic behind it?
                Time = DateTime.Now.AddSeconds(-i * 10)
            };
            // Add the assigned values to the list
            readings.Add(electricityReading);
        }
        // I'm ordering the reading outcome
        readings.Sort((reading1, reading2) => reading1.Time.CompareTo(reading2.Time));
        // And return the result
        return readings;
    }
}