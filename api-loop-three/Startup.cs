using ApiLoopThree.Domain;
using ApiLoopThree.Generator;
using ApiLoopThree.Services;

// 2. ENTRY POINT
namespace ApiLoopThree;

public class Startup
{
    private const string MOST_EVIL_PRICE_PLAN_ID = "price-plan-0";
    private const string RENEWABLES_PRICE_PLAN_ID = "price-plan-1";
    private const string STANDARD_PRICE_PLAN_ID = "price-plan-2";

    /* The IConfiguration object (part of Microsoft.Extensions.Configuration namespace)
       is injected by the ASP.NET Core runtime.
       It provides access to configuration settings from appsettings.json. */
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime.
    // Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // The variable saves a method
        var readings = GenerateMeterElectricityReadings();

        // The list from Domain > PricePlan.cs conforms an object
        // gathering the elements from different places
        var pricePlans = new List<PricePlan>
        {
            new PricePlan
            {
                // It comes from the constant declaration on top
                PlanName = MOST_EVIL_PRICE_PLAN_ID,
                // It comes from Enums > Supplier
                EnergySupplier = Enums.Supplier.DrEvilsDarkEnergy,
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

        // Adds MVC services and disables legacy Endpoint Routing
        services.AddMvc(options => options.EnableEndpointRouting = false);
        // Creates a new instance of the service every time requested
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IMeterReadingService, MeterReadingService>();
        services.AddTransient<IPricePlanService, PricePlanService>();
        // Creates and share a single instance for the entire app lifetime
        services.AddSingleton((IServiceProvider arg) => readings);
        services.AddSingleton((IServiceProvider arg) => pricePlans);
        services.AddSingleton((IServiceProvider arg) => SmartMeterToPricePlanAccounts);
    }

    // This method gets called by the runtime.
    // Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMvc();
    }

    // Maps each Smart Meter ID (string) to a list
    // of generated electricity readings
    private Dictionary<string, List<ElectricityReading>> GenerateMeterElectricityReadings()
    {
        var readings = new Dictionary<string, List<ElectricityReading>>();
        var generator = new ElectricityReadingGenerator();
        var smartMeterIds = SmartMeterToPricePlanAccounts.Select(mtpp => mtpp.Key);

        foreach (var smartMeterId in smartMeterIds)
        {
            readings.Add(smartMeterId, generator.Generate(20));
        }

        return readings;
    }

    // Maps each Smart Meter ID to a Price Plan ID
    public Dictionary<string, string> SmartMeterToPricePlanAccounts
    {
        get
        {
            Dictionary<string, string> smartMeterToPricePlanAccounts = new Dictionary<string, string>();
            
            smartMeterToPricePlanAccounts.Add("smart-meter-0", MOST_EVIL_PRICE_PLAN_ID);
            smartMeterToPricePlanAccounts.Add("smart-meter-1", RENEWABLES_PRICE_PLAN_ID);
            smartMeterToPricePlanAccounts.Add("smart-meter-2", MOST_EVIL_PRICE_PLAN_ID);
            smartMeterToPricePlanAccounts.Add("smart-meter-3", STANDARD_PRICE_PLAN_ID);
            smartMeterToPricePlanAccounts.Add("smart-meter-4", RENEWABLES_PRICE_PLAN_ID);

            return smartMeterToPricePlanAccounts;
        }
    }
}