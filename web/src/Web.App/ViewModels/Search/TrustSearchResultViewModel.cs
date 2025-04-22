using System.Text;
using Web.App.Domain;

// ReSharper disable MemberCanBePrivate.Global

namespace Web.App.ViewModels.Search;

public record TrustSearchResultViewModel
{
    public string? CompanyNumber { get; init; }
    public string? TrustName { get; init; }
    public double? TotalPupils { get; init; }
    public double? SchoolsInTrust { get; init; }

    public static TrustSearchResultViewModel Create(TrustSummary trust)
    {
        return new TrustSearchResultViewModel
        {
            CompanyNumber = trust.CompanyNumber,
            TrustName = trust.TrustName,
            TotalPupils = trust.TotalPupils,
            SchoolsInTrust = trust.SchoolsInTrust,
        };
    }
}