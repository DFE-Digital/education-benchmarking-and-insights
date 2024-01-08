namespace EducationBenchmarking.Web.Integration.Tests;

public static class Paths
{
    public const string ServiceHome = "/";
    public const string FindOrganisation = "/find-organisation";
    public const string Error = "/error";
    
    public static string StatusError(int statusCode) => $"/error/{statusCode}";
    public static string SchoolHome(string urn) => $"/school/{urn}";
    public static string SchoolComparison(string urn) => $"/school/{urn}/comparison";
    public static string SchoolWorkforce(string urn) => $"/school/{urn}/workforce";
    
    public static string ToAbsolute(this string path)
    {
        return $"https://localhost{path}";
    }
}