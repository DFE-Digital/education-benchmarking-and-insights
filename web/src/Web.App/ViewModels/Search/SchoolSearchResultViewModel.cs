namespace Web.App.ViewModels.Search;

public record SchoolSearchResultViewModel
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? AddressStreet { get; set; }
    public string? AddressLocality { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressTown { get; set; }
    public string? AddressCounty { get; set; }
    public string? AddressPostcode { get; set; }
}