using System.Collections.Generic;
using System.Linq;
using Platform.Api.Insight.Features.BudgetForecast.Models;

namespace Platform.Api.Insight.Features.BudgetForecast.Responses;

public static class BudgetForecastReturnsResponseFactory
{
    public static BudgetForecastReturnResponse[] CreateForDefaultRunType(IEnumerable<BudgetForecastReturnModel> bfr, IEnumerable<ActualReturnModel> ar)
    {
        var results = bfr.Distinct().ToDictionary(x => x.Year, x => new BudgetForecastReturnResponse
        {
            Year = x.Year,
            Forecast = x.Value,
            ForecastTotalPupils = x.TotalPupils
        });

        foreach (var accountReturn in ar)
        {
            if (results.TryGetValue(accountReturn.Year, out var response))
            {
                response.Actual = accountReturn.Value;
                response.ActualTotalPupils = accountReturn.TotalPupils;
            }
        }

        return results.Values.ToArray();
    }

    public static BudgetForecastReturnMetricResponse Create(BudgetForecastReturnMetricModel model)
    {
        var response = new BudgetForecastReturnMetricResponse
        {
            Year = model.Year,
            Metric = model.Metric,
            Value = model.Value
        };

        return response;
    }
}