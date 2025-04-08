using System.Text;
using Web.App.Domain;

// ReSharper disable MemberCanBePrivate.Global

namespace Web.App.ViewModels.Search;

public record SchoolSearchResultViewModel
{
    public string? URN { get; init; }
    public string? SchoolName { get; init; }
    public string? AddressStreet { get; init; }
    public string? AddressLocality { get; init; }
    public string? AddressLine3 { get; init; }
    public string? AddressTown { get; init; }
    public string? AddressCounty { get; init; }
    public string? AddressPostcode { get; init; }
    public string? Address { get; init; }
    public string? OverallPhase { get; init; }
    public int? PeriodCoveredByReturn { get; init; }
    public double? TotalPupils { get; init; }

    public static SchoolSearchResultViewModel Create(SchoolSummary school)
    {
        var components = new[]
        {
            school.AddressStreet?.Replace(school.SchoolName!, string.Empty).TrimStart(',').Trim(),
            school.AddressLocality?.Replace(school.SchoolName!, string.Empty).TrimStart(',').Trim(),
            school.AddressLine3?.Replace(school.SchoolName!, string.Empty).TrimStart(',').Trim(),
            school.AddressTown,
            school.AddressCounty,
            school.AddressPostcode
        }.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        var address = new StringBuilder(components.First());
        for (var i = 1; i < components.Length; i++)
        {
            var component = components.ElementAt(i);
            if (component == components.ElementAt(i - 1))
            {
                continue;
            }

            address.Append(", ");
            address.Append(component);
        }

        return new SchoolSearchResultViewModel
        {
            URN = school.URN,
            SchoolName = school.SchoolName,
            AddressStreet = school.AddressStreet,
            AddressLocality = school.AddressLocality,
            AddressLine3 = school.AddressLine3,
            AddressTown = school.AddressTown,
            AddressCounty = school.AddressCounty,
            AddressPostcode = school.AddressPostcode,
            Address = address.ToString(),
            OverallPhase = school.OverallPhase,
            PeriodCoveredByReturn = school.PeriodCoveredByReturn,
            TotalPupils = school.TotalPupils
        };
    }
}