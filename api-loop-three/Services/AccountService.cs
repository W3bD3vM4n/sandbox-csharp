namespace ApiLoopThree.Services;

public class AccountService : IAccountService
{
    // Mapping between smart meter IDs (keys) and price plan IDs (values)
    // to look up which plan applies to a given meter
    private Dictionary<string, string> _smartMeterToPricePlanAccounts;

    // Constructor injection to receive the dictionary
    // from the DI container (registered as a singleton earlier)
    public AccountService(Dictionary<string, string> smartMeterToPricePlanAccounts)
    {
        _smartMeterToPricePlanAccounts = smartMeterToPricePlanAccounts;
    }

    public string GetPricePlanIdForSmartMeterId(string smartMeterId)
    {
        if (!_smartMeterToPricePlanAccounts.ContainsKey(smartMeterId))
        {
            return null;
        }
        
        // Return the associated price plan ID
        return _smartMeterToPricePlanAccounts[smartMeterId];
    }
}