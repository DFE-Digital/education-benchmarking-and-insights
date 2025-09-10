using Microsoft.FeatureManagement;
using Web.App.Domain;

namespace Web.App.Services;

public interface ICostCodesService
{
    Task<CostCodes> GetCostCodes(bool isPartOfTrust);
}

public class CostCodesService(IFeatureManager featureManager) : ICostCodesService
{
    // todo: consider IMemoryCache and registering as singleton as
    //       this is a one-off task each for CFR/AAR cost codes
    public async Task<CostCodes> GetCostCodes(bool isPartOfTrust) => new(isPartOfTrust, !isPartOfTrust && await featureManager.IsEnabledAsync(FeatureFlags.CfrItSpendBreakdown));
}