namespace Web.App.Domain;

public abstract record ItSpend
{
    public decimal? Connectivity { get; set; }
    public decimal? OnsiteServers { get; set; }
    public decimal? ItLearningResources { get; set; }
    public decimal? AdministrationSoftwareAndSystems { get; set; }
    public decimal? LaptopsDesktopsAndTablets { get; set; }
    public decimal? OtherHardware { get; set; }
    public decimal? ItSupport { get; set; }
}

public record SchoolItSpend : ItSpend, IEqualityComparer<SchoolItSpend>
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
    public decimal? TotalPupils { get; set; }

    public bool Equals(SchoolItSpend? x, SchoolItSpend? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null)
        {
            return false;
        }

        if (y is null)
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.URN == y.URN;
    }

    public int GetHashCode(SchoolItSpend obj)
    {
        return obj.URN != null ? obj.URN.GetHashCode() : 0;
    }
}