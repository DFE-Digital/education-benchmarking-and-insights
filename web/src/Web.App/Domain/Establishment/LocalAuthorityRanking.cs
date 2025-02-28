using System.Diagnostics.CodeAnalysis;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedMember.Global

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