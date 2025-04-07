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

    public static SchoolSearchResultViewModel Create(School school)
    {
        return new SchoolSearchResultViewModel
        {
            URN = school.URN,
            SchoolName = school.SchoolName,
            AddressStreet = school.AddressStreet,
            AddressLocality = school.AddressLocality,
            AddressLine3 = school.AddressLine3,
            AddressTown = school.AddressTown,
            AddressCounty = school.AddressCounty,
            AddressPostcode = school.AddressPostcode
        };
    }
}