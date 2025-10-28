using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class SchoolsWorkforceSummaryDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Workforce.Actuals => "SELECT /**select**/ FROM VW_SchoolsWorkforceSummaryDefaultCurrentActual /**where**/ /**orderby**/",
            Dimensions.Workforce.PercentPupil => "SELECT /**select**/ FROM VW_SchoolsWorkforceSummaryDefaultCurrentPercentPupil /**where**/ /**orderby**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}