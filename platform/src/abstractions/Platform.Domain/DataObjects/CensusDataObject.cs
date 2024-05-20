using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record CensusDataObject : QueryableFinancesDataObject
{
    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SCHOOL_NAME)]
    public string? SchoolName { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SCHOOL_TYPE)]
    public string? Type { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.LA)]
    public int La { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.NO_PUPILS)]
    public decimal NoPupils { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PERCENTAGE_QUALIFIED_TEACHERS)]
    public decimal PercentageQualifiedTeachers { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.WORKFORCE_TOTAL)]
    public decimal WorkforceTotal { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TEACHERS_TOTAL)]
    public decimal TeachersTotal { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TEACHERS_LEADER)]
    public decimal TeachersLeader { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FULL_TIME_TA)]
    public decimal FullTimeTa { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FULL_TIME_OTHER)]
    public decimal FullTimeOther { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.AUX_STAFF)]
    public decimal AuxStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.WORKFORCE_HEADCOUNT)]
    public decimal WorkforceHeadcount { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PERIOD_COVERED_BY_RETURN)]
    public int PeriodCoveredByReturn { get; set; }

}