using System.Text.RegularExpressions;
namespace Platform.Cache;

public partial class CacheKeyFactory : ICacheKeyFactory
{
    public string CreateCensusHistoryCacheKey(string overallPhase, string financeType, string dimension) =>
        CreateCacheKey("census", "history", "national-average", $"{overallPhase}|{financeType}|{dimension}");

    public string CreateExpenditureHistoryCacheKey(string overallPhase, string financeType, string dimension) =>
        CreateCacheKey("expenditure", "history", "national-average", $"{overallPhase}|{financeType}|{dimension}");

    public string CreateCacheKey(params string[] parts) => string.Join(":", parts.Select(EncodeKeyPart));

    private static string? EncodeKeyPart(string? field) => string.IsNullOrWhiteSpace(field)
        ? field
        : NonAlphaNumericRegex().Replace(field.ToLower(), ".");

    [GeneratedRegex(@"[^a-zA-Z0-9\-\|]")]
    private static partial Regex NonAlphaNumericRegex();
}

public interface ICacheKeyFactory
{
    string CreateCensusHistoryCacheKey(string overallPhase, string financeType, string dimension);
    string CreateExpenditureHistoryCacheKey(string overallPhase, string financeType, string dimension);
    string CreateCacheKey(params string[] parts);
}