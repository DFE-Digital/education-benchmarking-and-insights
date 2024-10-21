namespace Platform.Sql;

public static partial class Queries
{
    public static string GetCurrentYear => "SELECT Value FROM Parameters WHERE Name = 'CurrentYear'";
    public static string GetLatestBfrYear => "SELECT Value FROM Parameters WHERE Name = 'LatestBFRYear'";
    
}