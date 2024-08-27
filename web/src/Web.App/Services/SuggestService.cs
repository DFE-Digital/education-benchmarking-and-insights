using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface ISuggestService
{
    Task<IEnumerable<SuggestValue<School>>> SchoolSuggestions(string search, string[]? excludeSchools = null);
    Task<IEnumerable<SuggestValue<Trust>>> TrustSuggestions(string search, string[]? excludeTrusts = null);
    Task<IEnumerable<SuggestValue<LocalAuthority>>> LocalAuthoritySuggestions(string search, string[]? excludeLas = null);
}

public class SuggestService(IEstablishmentApi establishmentApi) : ISuggestService
{
    public async Task<IEnumerable<SuggestValue<School>>> SchoolSuggestions(string search, string[]? excludeSchools = null)
    {
        var query = new ApiQuery();
        if (excludeSchools != null)
        {
            foreach (var school in excludeSchools)
            {
                query.AddIfNotNull("urns", school);
            }
        }

        var suggestions = await establishmentApi.SuggestSchools(search, query).GetResultOrThrow<SuggestOutput<School>>();
        return suggestions.Results.Select(SchoolSuggestValue);
    }

    public async Task<IEnumerable<SuggestValue<Trust>>> TrustSuggestions(string search, string[]? excludeTrusts = null)
    {
        var query = new ApiQuery();
        if (excludeTrusts != null)
        {
            foreach (var school in excludeTrusts)
            {
                query.AddIfNotNull("companyNumbers", school);
            }
        }

        var suggestions = await establishmentApi.SuggestTrusts(search, query).GetResultOrThrow<SuggestOutput<Trust>>();
        return suggestions.Results.Select(TrustSuggestValue);
    }

    private static SuggestValue<Trust> TrustSuggestValue(SuggestValue<Trust> value)
    {
        var text = value.Text?.Replace("*", "");

        var additionalText = "";

        if (!string.IsNullOrWhiteSpace(value.Document?.CompanyNumber))
            additionalText = text == value.Document.CompanyNumber ? $" ({value.Text})" : $" ({value.Document.CompanyNumber})";

        if (text != value.Document?.TrustName)
            value.Text = value.Document?.TrustName;

        value.Text = $"{value.Text}{additionalText}";

        return value;
    }

    public async Task<IEnumerable<SuggestValue<LocalAuthority>>> LocalAuthoritySuggestions(string search, string[]? excludeLas = null)
    {
        var query = new ApiQuery();
        if (excludeLas != null)
        {
            foreach (var la in excludeLas)
            {
                query.AddIfNotNull("names", la);
            }
        }

        var suggestions = await establishmentApi.SuggestLocalAuthorities(search, query).GetResultOrThrow<SuggestOutput<LocalAuthority>>();
        return suggestions.Results.Select(LocalAuthoritySuggestValue);
    }

    private static SuggestValue<LocalAuthority> LocalAuthoritySuggestValue(SuggestValue<LocalAuthority> value)
    {
        var text = value.Text?.Replace("*", "");

        if (text != value.Document?.Name)
        {
            value.Text = value.Document?.Name;
        }

        return value;
    }


    private static SuggestValue<School> SchoolSuggestValue(SuggestValue<School> value)
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

    private static List<string?> SchoolAdditionalDetails(SuggestValue<School> value, string? text)
    {

        return text == value.Document?.URN
            ? [value.Text]
            : SchoolAddressAdditionalDetails(value, text);
    }

    private static List<string?> SchoolAddressAdditionalDetails(SuggestValue<School> value, string? text)
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