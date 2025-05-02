using System.Collections.Generic;
using System.Linq;
using Platform.Api.Insight.Features.BudgetForecast.Models;
using Platform.Api.Insight.Features.BudgetForecast.Responses;

namespace Platform.Api.Insight.Features.BudgetForecast;

public static class Mapper
{
    public static BudgetForecastReturnResponse[] MapToApiResponse(IEnumerable<BudgetForecastReturnModel> bfr, IEnumerable<ActualReturnModel> ar)
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

    public static BudgetForecastReturnMetricResponse MapToApiResponse(BudgetForecastReturnMetricModel model)
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