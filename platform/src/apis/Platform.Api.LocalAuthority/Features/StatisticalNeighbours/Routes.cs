namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours;

public static class Routes
{
    public const string StatisticalNeighbours = "local-authorities/{code:regex(^\\d{{3}}$)}/statistical-neighbours";
}