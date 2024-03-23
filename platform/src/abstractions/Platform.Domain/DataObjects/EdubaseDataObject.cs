using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record EdubaseDataObject
{
    [JsonProperty(PropertyName = EdubaseFieldNames.URN)]
    public long Urn { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.ESTAB_NAME)]
    public string? EstablishmentName { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.OVERALL_PHASE)]
    public string? OverallPhase { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.PHASE_OF_EDUCATION)]
    public string? PhaseOfEducation { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.TYPE_OF_ESTAB)]
    public string? TypeOfEstablishment { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.ADDRESS)]
    public string? Address { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.TRUSTS)]
    public string? Trusts { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.SPONSORS)]
    public string? SponsorName { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.COMPANY_NUMBER)]
    public int? CompanyNumber { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.TRUST_NAME)]
    public string? TrustOrCompanyName { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.UID)]
    public int? Uid { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.LA_CODE)]
    public int LaCode { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.LA_NAME)]
    public string? La { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.ESTAB_NO)]
    public int EstablishmentNumber { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.LA_ESTAB)]
    public int LaEstab { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.TEL_NO)]
    public string? TelephoneNum { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.NO_PUPIL)]
    public float? NumberOfPupils { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.STAT_LOW)]
    public int? StatutoryLowAge { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.STAT_HIGH)]
    public int? StatutoryHighAge { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.HEAD_FIRST_NAME)]
    public string? HeadFirstName { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.NURSERY_PROVISION)]
    public string? NurseryProvision { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.HEAD_LAST_NAME)]
    public string? HeadLastName { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.OFFICIAL_6_FORM_CODE)]
    public int? OfficialSixthFormCode { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.OFFICIAL_6_FORM)]
    public string? OfficialSixthForm { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.SCHOOL_WEB_SITE)]
    public string? SchoolWebsite { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.OFSTED_RATING)]
    public string? OfstedRating { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.OFSTE_LAST_INSP)]
    public string? OfstedLastInsp { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.FINANCE_TYPE)]
    public string? FinanceType { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.OPEN_DATE)]
    public string? OpenDate { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.CLOSE_DATE)]
    public string? CloseDate { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.RELIGIOUS_CHARACTER)]
    public string? ReligiousCharacter { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.MAT_SAT)]
    public string? MatSat { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.IS_FEDERATION)]
    public bool IsFederation { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.IS_PART_OF_FEDERATION)]
    public bool IsPartOfFederation { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.FEDERATION_MEMBERS)]
    public long[]? FederationMembers { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.FEDERATION_UID)]
    public long? FederationUid { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.FEDERATION_NAME)]
    public string? FederationName { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.FEDERATIONS_CODE)]
    public long? FederationsCode { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.FEDERATION)]
    public string? Federation { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.GOV_OFFICE_REGION)]
    public string? GovernmentOfficeRegion { get; set; }

    [JsonProperty(PropertyName = EdubaseFieldNames.ESTAB_STATUS)]
    public string? EstablishmentStatus { get; set; }
}