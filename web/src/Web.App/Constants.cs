namespace Web.App;

public static class Constants
{
    public const string ServiceName = "Financial Benchmarking and Insights Tool";
    public const string SupportEmail = "benchmarking.support@education.gov.uk";
    public const string CookieSettingsName = "cookie_policy";

    public static int CurrentYear => DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    public static IEnumerable<int> AvailableYears => Enumerable.Range(CurrentYear, 4).ToArray();

    public static string? GiasSchoolUrl(string? urn) => string.IsNullOrWhiteSpace(urn)
        ? null
        : $"https://www.get-information-schools.service.gov.uk/establishments/establishment/details/{urn}";
    public static string? GiasTrustUrl(string? uid) => string.IsNullOrWhiteSpace(uid)
        ? null
        : $"https://www.get-information-schools.service.gov.uk/groups/group/details/{uid}";
}