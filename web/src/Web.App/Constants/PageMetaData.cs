namespace Web.App;

public static class PageViewTelemetries
{
    public static PageViewTelemetry SchoolHome(string urn, bool? isLoggedIn) => new()
    {
        Name = "Your school",
        IsLoggedIn = isLoggedIn,
        Properties = new Dictionary<string, object>
        {
            {
                "URN", urn
            }
        }
    };
}

// https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/IPageViewTelemetry.ts
public record PageViewTelemetry
{
    /// <summary>
    ///     The string you used as the name in startTrackPage. Defaults to the document title.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     A relative or absolute URL that identifies the page or other item. Defaults to the window location.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    ///     The URL of the source page where current page is loaded from.
    /// </summary>
    public string? RefUri { get; set; }

    /// <summary>
    ///     Page Type string. Describes how you classify this page, e.g. errorPage, formPage, etc.
    /// </summary>
    public string? PageType { get; set; }

    /// <summary>
    ///     Whether or not the user is logged in
    /// </summary>
    public bool? IsLoggedIn { get; set; }

    /// <summary>
    ///     Property bag to contain additional custom properties (Part C)
    /// </summary>
    public Dictionary<string, object>? Properties { get; set; }

    /// <summary>
    ///     Custom defined iKey.
    /// </summary>
    public string? IKey { get; set; }

    /// <summary>
    ///     Time first page view is triggered
    /// </summary>
    public DateTime? StartTime { get; set; }
}