using ApiLoopThree.Domain;
using ApiLoopThree.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoopThree.Controllers;

[Route("readings")]
public class MeterReadingController : Controller
{
    // The DI from Services
    private readonly IMeterReadingService _meterReadingService;

    // Constructor
    public MeterReadingController(IMeterReadingService meterReadingService)
    {
        _meterReadingService = meterReadingService;
    }

    [HttpPost("store")]
    // [FromBody] tells ASP.NET Core to deserialize the HTTP request body
    // (likely JSON) into that MeterReadings object
    public ObjectResult Post([FromBody] MeterReadings meterReadings)
    {
        // Checks for null or empty values
        if (!IsMeterReadingsValid(meterReadings))
        {
            // One of several built‑in IActionResult implementations
            // for error responses (there are about a dozen)
            return new BadRequestObjectResult("Internal Server Error");
        }
        _meterReadingService.StoreReadings(
            meterReadings.SmartMeterId,
            meterReadings.ElectricityReadings
            );

        // IActionResult implementation representing HTTP 200 OK
        return new OkObjectResult("{}");
    }

    // Prevents downstream errors (e.g., null refs or storing empty data)
    private bool IsMeterReadingsValid(MeterReadings meterReadings)
    {
        string smartMeterId = meterReadings.SmartMeterId;
        List<ElectricityReading> electricityReadings = meterReadings.ElectricityReadings;
        
        // Ensure an ID was provided and the ID string isn't empty
        return smartMeterId != null && smartMeterId.Any()
                                    // and the list itself isn't null
                                    && electricityReadings != null
                                    // and the list has at least one reading
                                    && electricityReadings.Any();
    }

    // Maps URLs
    [HttpGet("read/{smartMeterId}")]
    // The parameter is populated from the URL placeholder
    public ObjectResult GetReading(string smartMeterId)
    {
        // Returns a new object list from the action method
        // with a "200 OK" response
        return new OkObjectResult(
            _meterReadingService.GetReadings(smartMeterId)
            );
    }
}