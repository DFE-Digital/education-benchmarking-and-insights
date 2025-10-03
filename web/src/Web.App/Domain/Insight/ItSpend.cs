// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

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

    public int GetHashCode(SchoolItSpend obj) => obj.URN != null ? obj.URN.GetHashCode() : 0;
}

public record TrustItSpend : ItSpend, IEqualityComparer<TrustItSpend>
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }

    public bool Equals(TrustItSpend? x, TrustItSpend? y)
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

        return x.CompanyNumber == y.CompanyNumber;
    }

    public int GetHashCode(TrustItSpend obj) => obj.CompanyNumber != null ? obj.CompanyNumber.GetHashCode() : 0;
}

public record TrustItSpendForecastYear : ItSpend
{
    public int? Year { get; set; }
}