namespace ApiLoopThree.Services;

public class AccountService : IAccountService
{
    private Dictionary<string, string> _smartMeterToPricePlanAccounts;

    // Constructor
    public AccountService(Dictionary<string, string> smartMeterToPricePlanAccounts)
    {
        _smartMeterToPricePlanAccounts = smartMeterToPricePlanAccounts;
    }

    // Method (what's the logic behind it?)
    public string GetPricePlanIdForSmartMeterId(string smartMeterId)
    {
        if (!_smartMeterToPricePlanAccounts.ContainsKey(smartMeterId))
        {
            return null;
        }
        
        // I'm returning and ID from a Dictionary?
        return _smartMeterToPricePlanAccounts[smartMeterId];
    }
}