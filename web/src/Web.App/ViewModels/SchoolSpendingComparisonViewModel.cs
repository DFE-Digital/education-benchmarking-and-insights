using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolSpendingComparisonViewModel
{
    private readonly RagRating[] _originalRating;
    private readonly RagRating[] _customRating;

    public SchoolSpendingComparisonViewModel(School school, RagRating[] originalRating, RagRating[] customRating)

    {
        Name = school.SchoolName;
        Urn = school.URN;
        _originalRating = originalRating;
        _customRating = customRating;
        GroupedComparisonResultsNoChange = GetGroupedComparisonResults(true);
        GroupedComparisonResultsChange = GetGroupedComparisonResults(false);
        HighHeadline = GetHeadlineResults("red");
        MediumHeadline = GetHeadlineResults("amber");
        LowHeadline = GetHeadlineResults("green");
    }
    public string? Name { get; }
    public string? Urn { get; }
    public (int OriginalCount, int CustomCount, string Change) HighHeadline { get; }
    public (int OriginalCount, int CustomCount, string Change) MediumHeadline { get; }
    public (int OriginalCount, int CustomCount, string Change) LowHeadline { get; }
    public List<ComparisonResult> GroupedComparisonResultsNoChange { get; }
    public List<ComparisonResult> GroupedComparisonResultsChange { get; }

    private List<ComparisonResult> GetGroupedComparisonResults(bool match)
    {
        var results = new List<ComparisonResult>();

        foreach (var originalItem in _originalRating)
        {
            var customItem = _customRating.FirstOrDefault(c => c.Category == originalItem.Category);

            bool isMatch = originalItem.PriorityTag == customItem?.PriorityTag;

            if ((match && isMatch) || (!match && !isMatch))
            {
                var result = new ComparisonResult
                {
                    OriginalPriorityTag = originalItem.PriorityTag,
                    CustomPriorityTag = customItem?.PriorityTag,
                    OriginalValue = originalItem.Value,
                    CustomValue = customItem?.Value,
                    OriginalPercentile = originalItem.Percentile,
                    CustomPercentile = customItem?.Percentile,
                    OriginalRAG = originalItem.RAG,
                    Category = originalItem.Category,
                };

                results.Add(result);
            }
        }

        return results.OrderBy(r => Lookups.StatusOrderMap[r.OriginalRAG ?? string.Empty])
            .ThenBy(r => r.OriginalValue)
            .ToList();
    }

    private (int OriginalCount, int CustomCount, string Change) GetHeadlineResults(string rag)
    {
        var originalCount = _originalRating.Where(c => c.RAG == rag).Count();
        var customCount = _customRating.Where(c => c.RAG == rag).Count();
        var change = originalCount > customCount
            ? ChangeSymbols.Decrease : originalCount < customCount ? ChangeSymbols.Increase
            : ChangeSymbols.NoChange;

        return (originalCount, customCount, change);
    }
}
public class ComparisonResult
{
    public (TagColour Colour, string DisplayText, string Class)? OriginalPriorityTag { get; set; }
    public (TagColour Colour, string DisplayText, string Class)? CustomPriorityTag { get; set; }
    public decimal? OriginalValue { get; set; }
    public decimal? CustomValue { get; set; }
    public decimal? OriginalPercentile { get; set; }
    public decimal? CustomPercentile { get; set; }
    public string? OriginalRAG { get; set; }
    public string? Category { get; set; }
}

public static class ChangeSymbols
{
    public const string Decrease = "▼";
    public const string Increase = "▲";
    public const string NoChange = "";
}

public class CategoryListSectionViewModel
{
    public string? Heading { get; init; }
    public List<ComparisonResult> CategoryList { get; init; } = [];
}
