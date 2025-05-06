using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface ISuggestService
{
    Task<IEnumerable<SuggestValue<SchoolSummary>>> SchoolSuggestions(string search, string[]? excludeSchools = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<SuggestValue<TrustSummary>>> TrustSuggestions(string search, string[]? excludeTrusts = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<SuggestValue<LocalAuthority>>> LocalAuthoritySuggestions(string search, string[]? excludeLas = null, CancellationToken cancellationToken = default);
}

public class SuggestService(IEstablishmentApi establishmentApi) : ISuggestService
{
    public async Task<IEnumerable<SuggestValue<SchoolSummary>>> SchoolSuggestions(string search, string[]? excludeSchools = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default)
    {
        var suggestions = await establishmentApi.SuggestSchools(search, excludeSchools, excludeMissingFinancialData, cancellationToken).GetResultOrThrow<SuggestOutput<SchoolSummary>>();
        return suggestions.Results.Select(SchoolSuggestValue);
    }

    public async Task<IEnumerable<SuggestValue<TrustSummary>>> TrustSuggestions(string search, string[]? excludeTrusts = null, CancellationToken cancellationToken = default)
    {
        var suggestions = await establishmentApi.SuggestTrusts(search, excludeTrusts, cancellationToken).GetResultOrThrow<SuggestOutput<TrustSummary>>();
        return suggestions.Results.Select(TrustSuggestValue);
    }

    public async Task<IEnumerable<SuggestValue<LocalAuthority>>> LocalAuthoritySuggestions(string search, string[]? excludeLas = null, CancellationToken cancellationToken = default)
    {
        var suggestions = await establishmentApi.SuggestLocalAuthorities(search, excludeLas, cancellationToken).GetResultOrThrow<SuggestOutput<LocalAuthority>>();
        return suggestions.Results.Select(LocalAuthoritySuggestValue);
    }

    private static SuggestValue<TrustSummary> TrustSuggestValue(SuggestValue<TrustSummary> value)
    {
        var text = value.Text?.Replace("*", "");

        var additionalText = "";

        if (!string.IsNullOrWhiteSpace(value.Document?.CompanyNumber))
        {
            additionalText = text == value.Document.CompanyNumber ? $" ({value.Text})" : $" ({value.Document.CompanyNumber})";
        }

        if (text != value.Document?.TrustName)
        {
            value.Text = value.Document?.TrustName;
        }

        value.Text = $"{value.Text}{additionalText}";

        return value;
    }

    private static SuggestValue<LocalAuthority> LocalAuthoritySuggestValue(SuggestValue<LocalAuthority> value)
    {
        var text = value.Text?.Replace("*", "");

        var additionalText = "";

        if (!string.IsNullOrWhiteSpace(value.Document?.Code))
        {
            additionalText = text == value.Document.Code ? $" ({value.Text})" : $" ({value.Document.Code})";
        }

        if (text != value.Document?.Name)
        {
            value.Text = value.Document?.Name;
        }

        value.Text = $"{value.Text}{additionalText}";

        return value;
    }


    private static SuggestValue<SchoolSummary> SchoolSuggestValue(SuggestValue<SchoolSummary> value)
    {
        var text = value.Text?.Replace("*", "");

        var additionalDetails = SchoolAdditionalDetails(value, text);

        if (text != value.Document?.SchoolName)
        {
            value.Text = value.Document?.SchoolName;
        }

        var additionalText = additionalDetails.Count > 0
            ? $" ({string.Join(", ", additionalDetails.Select(a => a))})"
            : "";

        value.Text = $"{value.Text}{additionalText}";

        return value;
    }

    private static List<string?> SchoolAdditionalDetails(SuggestValue<SchoolSummary> value, string? text)
    {

        return text == value.Document?.URN
            ? [value.Text]
            : SchoolAddressAdditionalDetails(value, text);
    }

    private static List<string?> SchoolAddressAdditionalDetails(SuggestValue<SchoolSummary> value, string? text)
    {
        var additionalDetails = new List<string?>();

        if (!string.IsNullOrWhiteSpace(value.Document?.AddressStreet) && text == value.Document.AddressStreet)
        {
            additionalDetails.Add(value.Text);
        }

        if (!string.IsNullOrWhiteSpace(value.Document?.AddressLocality) && text == value.Document.AddressLocality)
        {
            additionalDetails.Add(value.Text);
        }

        if (!string.IsNullOrWhiteSpace(value.Document?.AddressLine3) && text == value.Document.AddressLine3)
        {
            additionalDetails.Add(value.Text);
        }

        if (!string.IsNullOrWhiteSpace(value.Document?.AddressTown))
        {
            additionalDetails.Add(text == value.Document.AddressTown ? value.Text : value.Document.AddressTown);
        }

        if (!string.IsNullOrWhiteSpace(value.Document?.AddressCounty) && text == value.Document.AddressCounty)
        {
            additionalDetails.Add(value.Text);
        }

        if (!string.IsNullOrWhiteSpace(value.Document?.AddressPostcode))
        {
            additionalDetails.Add(text == value.Document.AddressPostcode ? value.Text : value.Document.AddressPostcode);
        }

        return additionalDetails;
    }
}