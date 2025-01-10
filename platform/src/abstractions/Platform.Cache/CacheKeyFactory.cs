using System.Text.RegularExpressions;
namespace Platform.Cache;

public partial class CacheKeyFactory : ICacheKeyFactory
{
    public string CreateCensusHistoryNationalAverageCacheKey(int endYear, string overallPhase, string financeType, string dimension) =>
        CreateHistoryCacheKey(endYear, "census", overallPhase, financeType, dimension);

    public string CreateExpenditureHistoryNationalAverageCacheKey(int endYear, string overallPhase, string financeType, string dimension) =>
        CreateHistoryCacheKey(endYear, "expenditure", overallPhase, financeType, dimension);

    public string CreateCacheKey(params string[] parts) => string.Join(":", parts.Select(EncodeKeyPart));

    private static string? EncodeKeyPart(string? field) => string.IsNullOrWhiteSpace(field)
        ? field
        : NonAlphaNumericRegex().Replace(field.ToLower(), ".");

    private string CreateHistoryCacheKey(int endYear, string type, string overallPhase, string financeType, string dimension) =>
        CreateCacheKey(endYear.ToString(), type, "history", "national-average", $"{overallPhase}|{financeType}|{dimension}");

    [GeneratedRegex(@"[^a-zA-Z0-9\-\|]")]
    private static partial Regex NonAlphaNumericRegex();
}

public interface ICacheKeyFactory
{
    string CreateCensusHistoryNationalAverageCacheKey(int endYear, string overallPhase, string financeType, string dimension);
    string CreateExpenditureHistoryNationalAverageCacheKey(int endYear, string overallPhase, string financeType, string dimension);
    string CreateCacheKey(params string[] parts);
}