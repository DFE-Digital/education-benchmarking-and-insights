using Web.App.Domain;
using Web.App.Extensions;

namespace Web.App.Services;

public interface ICustomDataService
{
    Task<CustomData> GetCurrentData(string urn);
    CustomData GetCustomDataFromSession(string urn);
    void SetCustomDataInSession(string urn, CustomData data);
    void ClearCustomDataFromSession(string urn);
}

public class CustomDataService(IHttpContextAccessor httpContextAccessor, IFinanceService financeService)
    : ICustomDataService
{
    public async Task<CustomData> GetCurrentData(string urn)
    {
        // todo: lookup and return current, potentially customised figures

        var finances = await financeService.GetFinances(urn);
        var income = await financeService.GetSchoolIncome(urn);
        var expenditure = await financeService.GetSchoolExpenditure(urn);
        var census = await financeService.GetSchoolCensus(urn);
        var floorArea = await financeService.GetSchoolFloorArea(urn);

        return new CustomData(finances, income, expenditure, census, floorArea);
    }

    public CustomData GetCustomDataFromSession(string urn)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        return context?.Session.Get<CustomData>(key) ?? new CustomData();
    }

    public void SetCustomDataInSession(string urn, CustomData data)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Set(key, data);
    }

    public void ClearCustomDataFromSession(string urn)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Remove(key);
    }
}