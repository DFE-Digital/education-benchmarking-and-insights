using Platform.Domain;

namespace Platform.Api.Benchmark.Db;

public static class FinancialPlanFactory
{
    public static FinancialPlanResponseModel CreateResponse(FinancialPlanDataObject dataObject)
    {
        return new FinancialPlanResponseModel
        {
            Year = dataObject.Year,
            Urn = dataObject.Urn,
            UpdatedAt = dataObject.UpdatedAt,
            UpdatedBy = dataObject.UpdatedBy,
            IsComplete = dataObject.IsComplete
        };
    }
}