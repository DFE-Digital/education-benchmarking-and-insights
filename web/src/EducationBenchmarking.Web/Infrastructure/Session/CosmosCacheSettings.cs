namespace EducationBenchmarking.Web.Infrastructure.Session;

public class CosmosCacheSettings
{
    public const string Section = nameof(CosmosCacheSettings);
    public string? ConnectionString { get; set; }
    public bool IsDirect { get; set; } = true;
    public string? ContainerName { get; set; }
    public string? DatabaseName { get; set; }
}