using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record LocalAuthorityRanking
{
    public LocalAuthorityRank[] Ranking { get; set; } = [];
}

[ExcludeFromCodeCoverage]
public record LocalAuthorityRank
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public int Rank { get; set; }
}