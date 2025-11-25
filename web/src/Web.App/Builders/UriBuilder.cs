using Microsoft.Extensions.Options;

namespace Web.App.Builders;

public interface IUriBuilder
{
    string? GiasSchoolUrl(string? urn);
    string? GiasTrustUrl(string? uid);
    string? CompareSchoolPerformanceUrl(string? urn);
}

public class UriBuilder(IOptions<UriOptions> options) : IUriBuilder
{
    private readonly string _giasBaseUrl = options.Value.GiasBaseUrl.TrimEnd('/');
    private readonly string _compareSchoolPerformanceBaseUrl = options.Value.CompareSchoolPerformanceBaseUrl.TrimEnd('/');

    public string? GiasSchoolUrl(string? urn) => string.IsNullOrWhiteSpace(urn)
        ? null
        : $"{_giasBaseUrl}/establishments/establishment/details/{Uri.EscapeDataString(urn)}";

    public string? GiasTrustUrl(string? uid) => string.IsNullOrWhiteSpace(uid)
        ? null
        : $"{_giasBaseUrl}/groups/group/details/{Uri.EscapeDataString(uid)}";

    public string? CompareSchoolPerformanceUrl(string? urn) => string.IsNullOrWhiteSpace(urn)
        ? null
        : $"{_compareSchoolPerformanceBaseUrl}/school/{Uri.EscapeDataString(urn)}";
}