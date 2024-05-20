using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record IncomeDataObject : QueryableFinancesDataObject
{

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.NO_PUPILS)]
    public decimal NoPupils { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TOTAL_EXP)]
    public decimal TotalExpenditure { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TOTAL_INCOME)]
    public decimal TotalIncome { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.GRANT_FUNDING)]
    public decimal GrantFunding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SELF_GENERATED_FUNDING)]
    public decimal SelfGeneratedFunding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.DIRECT_REVENUE_FINANCING)]
    public decimal DirectRevenueFinancing { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.DIRECT_GRANT)]
    public decimal DirectGrant { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PRE_POST_16_FUNDING)]
    public decimal PrePost16Funding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_DFE_GRANTS)]
    public decimal OtherDfeGrants { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_INCOME_GRANTS)]
    public decimal OtherIncomeGrants { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.GOVERNMENT_SOURCE)]
    public decimal GovernmentSource { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.COMMUNITY_GRANTS)]
    public decimal CommunityGrants { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ACADEMIES)]
    public decimal Academies { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.INCOME_FROM_FACILITIES)]
    public decimal IncomeFromFacilities { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.INCOME_FROM_CATERING)]
    public decimal IncomeFromCatering { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.DONATIONS)]
    public decimal Donations { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.RECEIPTS_FROM_SUPPLY)]
    public decimal ReceiptsFromSupply { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.INVESTMENT_INCOME)]
    public decimal InvestmentIncome { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_SELF_GENERATED)]
    public decimal OtherSelfGenerated { get; set; }
}