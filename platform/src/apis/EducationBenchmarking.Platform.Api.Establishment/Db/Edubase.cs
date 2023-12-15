using Newtonsoft.Json;

namespace EducationBenchmarking.Platform.Api.Establishment.Db;

public class Edubase
{
    [JsonProperty(PropertyName = FieldNames.URN)]
    public long URN { get; set; }

    [JsonProperty(PropertyName = FieldNames.ESTAB_NAME)]
    public string EstablishmentName { get; set; }

    [JsonProperty(PropertyName = FieldNames.OVERALL_PHASE)]
    public string OverallPhase { get; set; }

    [JsonProperty(PropertyName = FieldNames.PHASE_OF_EDUCATION)]
    public string PhaseOfEducation { get; set; }

    [JsonProperty(PropertyName = FieldNames.TYPE_OF_ESTAB)]
    public string TypeOfEstablishment { get; set; }

    /*[JsonProperty(PropertyName = FieldNames.LOCATION)]
    public LocationDataObject Location { get; set; }*/

    [JsonProperty(PropertyName = FieldNames.ADDRESS)]
    public string Address { get; set; }

    [JsonProperty(PropertyName = FieldNames.TRUSTS)]
    public string Trusts { get; set; }

    [JsonProperty(PropertyName = FieldNames.SPONSORS)]
    public string SponsorName { get; set; }

    [JsonProperty(PropertyName = FieldNames.COMPANY_NUMBER)]
    public int? CompanyNumber { get; set; }

    [JsonProperty(PropertyName = FieldNames.UID)]
    public int? UID { get; set; }

    [JsonProperty(PropertyName = FieldNames.LA_CODE)]
    public int LACode { get; set; }

    [JsonProperty(PropertyName = FieldNames.ESTAB_NO)]
    public int EstablishmentNumber { get; set; }

    [JsonProperty(PropertyName = FieldNames.LA_ESTAB)]
    public int LAEstab { get; set; }

    [JsonProperty(PropertyName = FieldNames.TEL_NO)]
    public string TelephoneNum { get; set; }

    [JsonProperty(PropertyName = FieldNames.NO_PUPIL)]
    public float? NumberOfPupils { get; set; }

    [JsonProperty(PropertyName = FieldNames.STAT_LOW)]
    public int? StatutoryLowAge { get; set; }

    [JsonProperty(PropertyName = FieldNames.STAT_HIGH)]
    public int? StatutoryHighAge { get; set; }

    [JsonProperty(PropertyName = FieldNames.HEAD_FIRST_NAME)]
    public string HeadFirstName { get; set; }

    [JsonProperty(PropertyName = FieldNames.NURSERY_PROVISION)]
    public string NurseryProvision { get; set; }

    [JsonProperty(PropertyName = FieldNames.HEAD_LAST_NAME)]
    public string HeadLastName { get; set; }

    [JsonProperty(PropertyName = FieldNames.OFFICIAL_6_FORM)]
    public string OfficialSixthForm { get; set; }

    [JsonProperty(PropertyName = FieldNames.SCHOOL_WEB_SITE)]
    public string SchoolWebsite { get; set; }

    [JsonProperty(PropertyName = FieldNames.OFSTED_RATING)]
    public string OfstedRating { get; set; }

    [JsonProperty(PropertyName = FieldNames.OFSTE_LAST_INSP)]
    public string OfstedLastInsp { get; set; }

    [JsonProperty(PropertyName = FieldNames.FINANCE_TYPE)]
    public string FinanceType { get; set; }

    [JsonProperty(PropertyName = FieldNames.OPEN_DATE)]
    public string OpenDate { get; set; }

    [JsonProperty(PropertyName = FieldNames.CLOSE_DATE)]
    public string CloseDate { get; set; }

    [JsonProperty(PropertyName = FieldNames.RELIGIOUS_CHARACTER)]
    public string ReligiousCharacter { get; set; }

    [JsonProperty(PropertyName = FieldNames.MAT_SAT)]
    public string MatSat { get; set; }

    [JsonProperty(PropertyName = FieldNames.IS_FEDERATION)]
    public bool IsFederation { get; set; }

    [JsonProperty(PropertyName = FieldNames.IS_PART_OF_FEDERATION)]
    public bool IsPartOfFederation { get; set; }

    [JsonProperty(PropertyName = FieldNames.FEDERATION_MEMBERS)]
    public long[] FederationMembers { get; set; }

    [JsonProperty(PropertyName = FieldNames.FEDERATION_UID)]
    public long? FederationUid { get; set; }

    [JsonProperty(PropertyName = FieldNames.FEDERATION_NAME)]
    public string FederationName { get; set; }

    [JsonProperty(PropertyName = FieldNames.FEDERATIONS_CODE)]
    public long? FederationsCode { get; set; }

    [JsonProperty(PropertyName = FieldNames.FEDERATION)]
    public string Federation { get; set; }

    [JsonProperty(PropertyName = FieldNames.GOV_OFFICE_REGION)]
    public string GovernmentOfficeRegion { get; set; }

    [JsonProperty(PropertyName = FieldNames.ESTAB_STATUS)]
    public string EstablishmentStatus { get; set; }
    
    public class LocationDataObject
    {
        [JsonProperty(PropertyName = FieldNames.LOCATION_TYPE)]
        public string type { get; set; }

        [JsonProperty(PropertyName = FieldNames.LOCATION_COORDINATES)]
        public string[] coordinates { get; set; }
    }
}