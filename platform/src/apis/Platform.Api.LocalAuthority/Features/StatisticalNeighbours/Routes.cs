using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours;

[ExcludeFromCodeCoverage]
public static class Routes
{
    public const string StatisticalNeighbours = $"local-authorities/{Constants.CodeParam}/statistical-neighbours";
}