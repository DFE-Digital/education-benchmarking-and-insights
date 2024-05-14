using Web.App.Domain;
using Web.App.Extensions;
using Web.App.ViewModels;

namespace Web.App.Services;

public interface ICustomDataService
{
    Task<CustomData> GetCurrentData(string urn);
    CustomData GetCustomDataFromSession(string urn);
    void MergeCustomDataIntoSession(string urn, IFinancialDataCustomDataViewModel data);
    void MergeCustomDataIntoSession(string urn, INonFinancialDataCustomDataViewModel data);
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

    public void MergeCustomDataIntoSession(string urn, IFinancialDataCustomDataViewModel viewModel)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        var data = context?.Session.Get<CustomData>(key) ?? new CustomData();
        data.Merge(viewModel);
        context?.Session.Set(key, data);
    }

    public void MergeCustomDataIntoSession(string urn, INonFinancialDataCustomDataViewModel viewModel)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        var data = context?.Session.Get<CustomData>(key) ?? new CustomData();
        data.Merge(viewModel);
        context?.Session.Set(key, data);
    }

    public void ClearCustomDataFromSession(string urn)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Remove(key);
    }
}