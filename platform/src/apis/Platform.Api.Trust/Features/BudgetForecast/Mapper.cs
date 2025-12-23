using System.Collections.Generic;
using System.Linq;
using Platform.Api.Trust.Features.BudgetForecast.Models;

namespace Platform.Api.Trust.Features.BudgetForecast;

public static class Mapper
{
    public static ForecastRiskResponse[] MapToApiResponse(IEnumerable<BudgetForecastReturnModelDto> bfr, IEnumerable<ActualReturnModelDto> ar)
    {
        var results = bfr.Distinct().ToDictionary(x => x.Year, x => new ForecastRiskResponse
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

    public static ForecastRiskMetricsResponse MapToApiResponse(BudgetForecastReturnMetricModelDto model)
    {
        var response = new ForecastRiskMetricsResponse
        {
            Year = model.Year,
            Metric = model.Metric,
            Value = model.Value
        };

        return response;
    }
}