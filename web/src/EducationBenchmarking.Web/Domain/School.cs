namespace EducationBenchmarking.Web.Domain;

public record School
{
    public string Urn { get; set; }
    public string Name { get; set; }
    public string FinanceType { get; set; }
    public string Kind { get; set; }
    public string LaEstab { get; set; }
    public string Street { get; set; }
    public string Locality { get; set; }
    public string Address3 { get; set; }
    public string Town { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string LocalAuthority { get; set; }
}