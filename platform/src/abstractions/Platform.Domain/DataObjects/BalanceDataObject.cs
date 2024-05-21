using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record BalanceDataObject : QueryableFinancesDataObject
{

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.NO_PUPILS)]
    public decimal NoPupils { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TOTAL_EXP)]
    public decimal TotalExpenditure { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TOTAL_INCOME)]
    public decimal TotalIncome { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.REVENUE_RESERVE)]
    public decimal RevenueReserve { get; set; }

}