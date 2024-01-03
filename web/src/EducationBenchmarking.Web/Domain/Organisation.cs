namespace EducationBenchmarking.Web.Domain;

public class Organisation
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Identifier { get; set; }
    public string Kind { get; set; }
    public string? Town { get; set; }
    public string? Postcode { get; set; }
}