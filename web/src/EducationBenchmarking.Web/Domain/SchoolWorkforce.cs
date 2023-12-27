namespace EducationBenchmarking.Web.Domain;

public record SchoolWorkforce
{
    public string Urn { get; set; }
    public string Name { get; set; }
    public string FinanceType { get; set; }
    public string LocalAuthority { get; set; }
    
}