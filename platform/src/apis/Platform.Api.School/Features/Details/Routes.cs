namespace Platform.Api.School.Features.Details;

public static class Routes
{
    public const string SchoolSingle = $"schools/{Constants.UrnParam}";
    public const string SchoolsCollection = "schools";
    public const string SchoolCharacteristics = $"schools/{Constants.UrnParam}/characteristics";
}