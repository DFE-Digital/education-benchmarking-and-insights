namespace Web.App.Domain.LocalAuthorities;

public record EducationHealthCarePlans
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? Total { get; set; }
    public decimal? Mainstream { get; set; }
    public decimal? Resourced { get; set; }
    public decimal? Special { get; set; }
    public decimal? Independent { get; set; }
    public decimal? Hospital { get; set; }
    public decimal? Post16 { get; set; }
    public decimal? Other { get; set; }
}