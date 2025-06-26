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
    // What kind of type is [FromBody]MeterReadings
    public ObjectResult Post([FromBody] MeterReadings meterReadings)
    {
        if (!IsMeterReadingsValid(meterReadings))
        {
            // How many kinds of methods like BadRequestObjectResult() exist?
            return new BadRequestObjectResult("Internal Server Error");
        }
        _meterReadingService.StoreReadings(meterReadings.SmartMeterId, meterReadings.ElectricityReadings);

        // What kind of return element is this?
        return new OkObjectResult("{}");
    }

    private bool IsMeterReadingsValid(MeterReadings meterReadings)
    {
        string smartMeterId = meterReadings.SmartMeterId;
        List<ElectricityReading> electricityReadings = meterReadings.ElectricityReadings;
        
        // Analize conditionals for the return
        return smartMeterId != null && smartMeterId.Any()
                                    && electricityReadings != null
                                    && electricityReadings.Any();
    }

    [HttpGet("read/{smartMeterId}")]
    public ObjectResult GetReading(string smartMeterId)
    {
        return new OkObjectResult(_meterReadingService.GetReadings(smartMeterId));
    }
}