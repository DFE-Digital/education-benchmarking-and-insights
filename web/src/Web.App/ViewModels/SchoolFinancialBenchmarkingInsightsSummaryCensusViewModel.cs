using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolFinancialBenchmarkingInsightsSummaryCensusViewModel(string name)
{
    public SchoolFinancialBenchmarkingInsightsSummaryCensusViewModel(string name, IEnumerable<Census>? census, string? urn, Func<Census, decimal?> selector) : this(name)
    {
        if (census == null)
        {
            return;
        }

        var enumerated = census.ToArray();

        if (!string.IsNullOrWhiteSpace(urn))
        {
            SchoolValue = enumerated
                .Where(c => c.URN == urn)
                .Select(selector)
                .FirstOrDefault();
        }

        MinValue = enumerated.Select(selector).Min();
        MaxValue = enumerated.Select(selector).Max();
    }

    public string Name => name;
    public decimal? SchoolValue { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
}