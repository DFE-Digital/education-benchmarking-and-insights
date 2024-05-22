using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface ISuggestService
{
    Task<IEnumerable<SuggestValue<School>>> SchoolSuggestions(string search);
    Task<IEnumerable<SuggestValue<Trust>>> TrustSuggestions(string search);
    Task<IEnumerable<SuggestValue<LocalAuthority>>> LocalAuthoritySuggestions(string search);
}

public class SuggestService(IEstablishmentApi establishmentApi) : ISuggestService
{
    public async Task<IEnumerable<SuggestValue<School>>> SchoolSuggestions(string search)
    {
        var suggestions = await establishmentApi.SuggestSchools(search).GetResultOrThrow<SuggestOutput<School>>();
        return suggestions.Results.Select(value =>
        {
            var text = value.Text?.Replace("*", "");

            var additionalDetails = new List<string?>();

            if (!string.IsNullOrWhiteSpace(value.Document?.Town))
                additionalDetails.Add(text == value.Document.Town ? value.Text : value.Document.Town);
            if (!string.IsNullOrWhiteSpace(value.Document?.Postcode))
                additionalDetails.Add(text == value.Document.Postcode ? value.Text : value.Document.Postcode);

            if (text != value.Document?.Name)
            {
                value.Text = value.Document?.Name;
            }

            var additionalText = additionalDetails.Count > 0
                ? $" ({string.Join(", ", additionalDetails.Select(a => a))})"
                : "";

            value.Text = $"{value.Text}{additionalText}";

            return value;
        });
    }

    public async Task<IEnumerable<SuggestValue<Trust>>> TrustSuggestions(string search)
    {
        var suggestions = await establishmentApi.SuggestTrusts(search).GetResultOrThrow<SuggestOutput<Trust>>();
        return suggestions.Results.Select(value =>
        {
            var text = value.Text?.Replace("*", "");

            if (text != value.Document?.Name)
            {
                value.Text = value.Document?.Name;
            }

            if (!string.IsNullOrWhiteSpace(value.Document?.CompanyNumber))
            {
                value.Text = $"{value.Document?.Name} ({value.Document?.CompanyNumber})";
            }

            return value;
        });
    }

    public async Task<IEnumerable<SuggestValue<LocalAuthority>>> LocalAuthoritySuggestions(string search)
    {
        var suggestions = await establishmentApi.SuggestLocalAuthorities(search).GetResultOrThrow<SuggestOutput<LocalAuthority>>();
        return suggestions.Results.Select(value =>
        {
            var text = value.Text?.Replace("*", "");

            if (text != value.Document?.Name)
            {
                value.Text = value.Document?.Name;
            }

            return value;
        });
    }
}