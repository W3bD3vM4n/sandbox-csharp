using ApiLoopThree.Domain;
using ApiLoopThree.Generator;
using ApiLoopThree.Services;

// 2. ENTRY POINT
namespace ApiLoopThree;

public class Startup
{
    // Constants [2.0]
    private const string MOST_EVIL_PRICE_PLAN_ID = "price-plan-0";
    private const string RENEWABLES_PRICE_PLAN_ID = "price-plan-1";
    private const string STANDARD_PRICE_PLAN_ID = "price-plan-2";

    // [TEMPLATE]
    // [2.2] The IConfiguration is injected by the ASP.NET Core runtime
    public Startup(IConfiguration configuration)
    {
        // Provide access to configuration from appsettings.json
        Configuration = configuration;
    }
    
    // [TEMPLATE]
    // [2.1] IConfiguration object (from Microsoft.Extensions.Configuration)
    public IConfiguration Configuration { get; }

    // Call by runtime, add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        // The variable saves a method from [2.4]
        var readings = GenerateMeterElectricityReadings();

        // The list from Domain > PricePlan.cs conforms an object
        // gathering the elements from different places
        var pricePlans = new List<PricePlan>
        {
            new PricePlan
            {
                // It comes from the constant declaration on top
                PlanName = MOST_EVIL_PRICE_PLAN_ID, // [2.0]
                // It comes from Enums > Supplier
                EnergySupplier = Enums.Supplier.DrEvilsDarkEnergy, // [3.0]
                // Hardcoded decimals manually assigned
                UnitRate = 10m,
                // It comes from Domain > PricePlan
                PeakTimeMultiplier = new List<PeakTimeMultiplier>()
            },
            new PricePlan
            {
                PlanName = RENEWABLES_PRICE_PLAN_ID,
                EnergySupplier = Enums.Supplier.TheGreenEco,
                UnitRate = 2m,
                PeakTimeMultiplier = new List<PeakTimeMultiplier>()
            },
            new PricePlan
            {
                PlanName = STANDARD_PRICE_PLAN_ID,
                EnergySupplier = Enums.Supplier.PowerForEveryone,
                UnitRate = 1m,
                PeakTimeMultiplier = new List<PeakTimeMultiplier>()
            }
        };

        // [TEMPLATE]
        // Adds MVC services and disables legacy Endpoint Routing
        services.AddMvc(options => options.EnableEndpointRouting = false);
        // [CUSTOM]
        // Creates a new instance of the service every time requested
        // Comes from [8.0]
        services.AddTransient<IAccountService, AccountService>();
        // Comes from [9.0]
        services.AddTransient<IMeterReadingService, MeterReadingService>();
        // Comes from [10.0]
        services.AddTransient<IPricePlanService, PricePlanService>();
        // Creates and share a single instance for the entire app lifetime
        services.AddSingleton((IServiceProvider arg) => readings);
        services.AddSingleton((IServiceProvider arg) => pricePlans);
        // Comes from [2.3]
        services.AddSingleton((IServiceProvider arg) => SmartMeterToPricePlanAccounts);
    }

    // [TEMPLATE]
    // Call by the runtime, configures the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMvc();
    }

    // [2.4] Maps each Smart Meter ID to a list of readings
    private Dictionary<string, List<ElectricityReading>> GenerateMeterElectricityReadings()
    {
        var readings = new Dictionary<string, List<ElectricityReading>>();
        var generator = new ElectricityReadingGenerator();
        // Comes from [2.3]
        var smartMeterIds = SmartMeterToPricePlanAccounts.Select(mtpp => mtpp.Key);

        foreach (var smartMeterId in smartMeterIds)
        {
            // Comes from [7.1]
            readings.Add(smartMeterId, generator.Generate(20));
        }

        return readings;
    }

    // [CUSTOM]
    // [2.3] Maps each Smart Meter ID to a Price Plan ID
    public Dictionary<string, string> SmartMeterToPricePlanAccounts
    {
        get
        {
            Dictionary<string, string> smartMeterToPricePlanAccounts = new Dictionary<string, string>();
            
            // Comes from [2.0]
            smartMeterToPricePlanAccounts.Add("smart-meter-0", MOST_EVIL_PRICE_PLAN_ID);
            smartMeterToPricePlanAccounts.Add("smart-meter-1", RENEWABLES_PRICE_PLAN_ID);
            smartMeterToPricePlanAccounts.Add("smart-meter-2", MOST_EVIL_PRICE_PLAN_ID);
            smartMeterToPricePlanAccounts.Add("smart-meter-3", STANDARD_PRICE_PLAN_ID);
            smartMeterToPricePlanAccounts.Add("smart-meter-4", RENEWABLES_PRICE_PLAN_ID);

            return smartMeterToPricePlanAccounts;
        }
    }
}